using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class OrderMapper : IModelToDtoMapper<Order, OrderDto>
{
    private readonly IModelToDtoMapper<User, GeneralUserDto> _userMapper;
    private readonly IModelToDtoMapper<OrderDetail, OrderDetailDto> _orderDetailMapper;

    public OrderMapper(IModelToDtoMapper<User, GeneralUserDto> userMapper, IModelToDtoMapper<OrderDetail, OrderDetailDto> orderDetailMapper)
    {
        _userMapper = userMapper;
        _orderDetailMapper = orderDetailMapper;
    }
    
    public OrderDto? ToDto(Order? model)
    {
        return (model != null) ? new OrderDto(
            model.Id, 
            _userMapper.ToDto(model.User), 
            _orderDetailMapper.ToListDtos(model.OrderDetails),
            model.CreateAt) : null;
    }
    
    public ICollection<OrderDto> ToListDtos(ICollection<Order>? models)
    {
        return models?.Select(model => ToDto(model)).ToList() ?? new List<OrderDto>();
    }
}