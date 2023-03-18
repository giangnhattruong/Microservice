using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface IOrderMapper
{
    OrderDto ToDto(Order model);
    ICollection<OrderDto> ToListDtos(ICollection<Order> models);
}