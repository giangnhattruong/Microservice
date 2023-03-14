using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface IUserOrderMapper
{
    Order ToModel(UserOrderDto dto);
    UserOrderDto ToDto(Order model);
    ICollection<Order> ToListModels(ICollection<UserOrderDto> dtos);
    ICollection<UserOrderDto> ToListDtos(ICollection<Order> models);
}