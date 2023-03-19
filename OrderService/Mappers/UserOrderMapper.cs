using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class UserOrderMapper : IModelToDtoMapper<Order, UserOrderDto>
{
    private readonly IModelToDtoMapper<OrderDetail, OrderDetailDto> _orderDetailMapper;

    public UserOrderMapper(IModelToDtoMapper<OrderDetail, OrderDetailDto> orderDetailMapper)
    {
        _orderDetailMapper = orderDetailMapper;
    }
    
    public UserOrderDto? ToDto(Order? model)
    {
        return (model != null) ? new UserOrderDto(
            model.Id, 
            _orderDetailMapper.ToListDtos(model.OrderDetails),
            model.CreateAt) : null;
    }
    
    public ICollection<UserOrderDto> ToListDtos(ICollection<Order>? models)
    {
        return models?.Select(model => ToDto(model)).ToList() ?? new List<UserOrderDto>();
    }
}