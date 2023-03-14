using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class OrderMapper : IOrderMapper
{
    private readonly IUserMapper _userMapper;
    private readonly IOrderDetailMapper _orderDetailMapper;

    public OrderMapper(IUserMapper userMapper, IOrderDetailMapper orderDetailMapper)
    {
        _userMapper = userMapper;
        _orderDetailMapper = orderDetailMapper;
    }
    
    public Order ToModel(OrderDto dto)
    {
        var model = new Order();
        model.Id = dto.Id;
        model.User = _userMapper.ToModel(dto.User);
        model.CreateAt = dto.CreatedAt;

        return model;
    }
    
    public OrderDto ToDto(Order model)
    {
        return new OrderDto(
            model.Id, 
            _userMapper.ToDto(model.User), 
            _orderDetailMapper.ToListDtos(model.OrderDetails),
            model.CreateAt);
    }
    
    public ICollection<Order> ToListModels(ICollection<OrderDto> dtos)
    {
        return dtos.Select(dto => ToModel(dto)).ToList();
    }
    
    public ICollection<OrderDto> ToListDtos(ICollection<Order> models)
    {
        return models.Select(model => ToDto(model)).ToList();
    }
}