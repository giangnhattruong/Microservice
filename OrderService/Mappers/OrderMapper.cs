using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class OrderMapper : IOrderMapper
{
    private readonly IGeneralUserMapper _userMapper;
    private readonly IOrderDetailMapper _orderDetailMapper;

    public OrderMapper(IGeneralUserMapper userMapper, IOrderDetailMapper orderDetailMapper)
    {
        _userMapper = userMapper;
        _orderDetailMapper = orderDetailMapper;
    }
    
    public OrderDto ToDto(Order model)
    {
        return new OrderDto(
            model.Id, 
            _userMapper.ToDto(model.User), 
            _orderDetailMapper.ToListDtos(model.OrderDetails),
            model.CreateAt);
    }
    
    public ICollection<OrderDto> ToListDtos(ICollection<Order> models)
    {
        return models.Select(model => ToDto(model)).ToList();
    }
}