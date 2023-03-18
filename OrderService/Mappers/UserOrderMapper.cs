using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class UserOrderMapper : IUserOrderMapper
{
    private readonly IOrderDetailMapper _orderDetailMapper;

    public UserOrderMapper(IOrderDetailMapper orderDetailMapper)
    {
        _orderDetailMapper = orderDetailMapper;
    }
    
    public UserOrderDto ToDto(Order model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        return new UserOrderDto(
            model.Id, 
            _orderDetailMapper.ToListDtos(model.OrderDetails),
            model.CreateAt);
    }
    
    public ICollection<UserOrderDto> ToListDtos(ICollection<Order> models)
    {
        return models.Select(model => ToDto(model)).ToList();
    }
}