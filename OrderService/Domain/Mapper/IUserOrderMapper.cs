using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface IUserOrderMapper
{
    UserOrderDto ToDto(Order model);
    ICollection<UserOrderDto> ToListDtos(ICollection<Order> models);
}