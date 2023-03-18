using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.Domain.Repositories;
using OrderService.Domain.Services;
using OrderService.DTOs;
using OrderService.Services.Communication;

namespace OrderService.Services;

public class OrderService : IOrderService
{
    private readonly IUserRepository _userRepository;
    private readonly IOrderRepository _orderRepository;
    private readonly IOrderDetailRepository _orderDetailRepository;
    private readonly IOrderMapper _orderMapper;
    private readonly ISaveOrderMapper _saveOrderMapper;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IUserRepository userRepository, 
        IOrderRepository orderRepository, 
        IOrderDetailRepository orderDetailRepository, 
        IOrderMapper orderMapper, 
        ISaveOrderMapper saveOrderMapper, 
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _orderRepository = orderRepository;
        _orderDetailRepository = orderDetailRepository;
        _orderMapper = orderMapper;
        _saveOrderMapper = saveOrderMapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<IEnumerable<OrderDto>?> ListAsync()
    {
        var orders = await _orderRepository.ListAsync();
        if (orders == null) return null;
        return _orderMapper.ToListDtos(orders);
    }
    
    public async Task<BaseResponse<OrderDto>> AddAsync(SaveOrderDto order)
    {
        try
        {
            var user = await _userRepository.GetAsync(order.UserId);
            if (user == null)
                return new BaseResponse<OrderDto>($"User is invalid.");
            
            var orderModel = _saveOrderMapper.ToModel(order);
            var orderDetails = orderModel.OrderDetails;
            orderModel.CreateAt = DateTime.Now;
            await _orderRepository.AddAsync(orderModel);

            foreach (var orderDetail in orderDetails)
            {
                orderDetail.OrderId = orderModel.Id;
                await _orderDetailRepository.AddAsync(orderDetail);
            }
            
            await _unitOfWork.CompleteAsync();
            
            // Get new resource
            var newOrder = await _orderRepository.GetAsync(orderModel.Id);

            return new BaseResponse<OrderDto>(_orderMapper.ToDto(newOrder));
        }
        catch (Exception ex)
        {
            return new BaseResponse<OrderDto>($"An error occurred: {ex.Message}\n Trace: {ex.StackTrace}");
        }
    }
    
    public async Task<BaseResponse<OrderDto>> UpdateAsync(int id, SaveOrderDto order)
    {
        try
        {
            var existingOrder = await _orderRepository.GetAsync(id);

            if (existingOrder == null)
                return new BaseResponse<OrderDto>("Data not found.");
            
            var user = await _userRepository.GetAsync(order.UserId);
            if (user == null)
                return new BaseResponse<OrderDto>($"User is invalid.");

            existingOrder.UserId = order.UserId;

            await _addOrRemoveOrderDetails(existingOrder, _saveOrderMapper.ToModel(order));
            
            await _unitOfWork.CompleteAsync();

            return new BaseResponse<OrderDto>(_orderMapper.ToDto(existingOrder));
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new BaseResponse<OrderDto>($"An error occurred: {ex.Message}");
        }
    }

    private async Task _addOrRemoveOrderDetails(Order oldOrder, Order newOrder)
    {
        var oldOrderDetails = oldOrder.OrderDetails;
        var newOrderDetails = newOrder.OrderDetails;

        var deletingOds = _getDifference(oldOrderDetails, newOrderDetails);
        _deleteOrderDetails(deletingOds);
        
        var addingOds = _getDifference(oldOrderDetails, newOrderDetails);
        await _addOrderDetails(oldOrder.Id, addingOds);
    }

    private ICollection<OrderDetail> _getDifference(ICollection<OrderDetail> first, ICollection<OrderDetail> second)
    {
        IList<OrderDetail> diff = new List<OrderDetail>();

        foreach (var fOd in first)
        {
            if (_doesExist(fOd, second)) continue;
            diff.Add(fOd);
        }

        return diff;
    }

    private bool _doesExist(OrderDetail orderDetail, ICollection<OrderDetail> orderDetails)
    {
        foreach (var od in orderDetails)
        {
            if (orderDetail.ProductId == od.ProductId &&
                orderDetail.Quantity == od.Quantity) return true;
        }

        return false;
    }

    private void _deleteOrderDetails(ICollection<OrderDetail> orderDetails)
    {
        foreach (var od in orderDetails)
        {
            _orderDetailRepository.Remove(od);
        }
    }   
    
    private async Task _addOrderDetails(int orderId, ICollection<OrderDetail> orderDetails)
    {
        foreach (var od in orderDetails)
        {
            od.OrderId = orderId;
            await _orderDetailRepository.AddAsync(od);
        }
    }

    public async Task<BaseResponse<OrderDto>> RemoveAsync(int id)
    {
        var existingOrder = await _orderRepository.GetAsync(id);

        if (existingOrder == null)
            return new BaseResponse<OrderDto>("Data not found.");

        try
        {
            _orderRepository.Remove(existingOrder);
            await _unitOfWork.CompleteAsync();

            return new BaseResponse<OrderDto>(_orderMapper.ToDto(existingOrder));
        }
        catch (Exception ex)
        {
            // Do some logging stuff
            return new BaseResponse<OrderDto>($"An error occurred: {ex.Message}");
        }
    }
    
}