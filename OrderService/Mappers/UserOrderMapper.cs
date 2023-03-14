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
    
    public Order ToModel(UserOrderDto dto)
    {
        var model = new Order();
        model.Id = dto.Id;
        model.OrderDetails = _orderDetailMapper.ToListModels(dto.Products);
        model.CreateAt = dto.CreatedAt;

        return model;
    }
    
    public UserOrderDto ToDto(Order model)
    {
        return new UserOrderDto(
            model.Id, 
            _orderDetailMapper.ToListDtos(model.OrderDetails),
            model.CreateAt);
    }
    
    public ICollection<Order> ToListModels(ICollection<UserOrderDto> dtos)
    {
        return dtos.Select(dto => ToModel(dto)).ToList();
    }
    
    public ICollection<UserOrderDto> ToListDtos(ICollection<Order> models)
    {
        return models.Select(model => ToDto(model)).ToList();
    }
}