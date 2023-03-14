using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface IOrderMapper
{
    Order ToModel(OrderDto dto);
    OrderDto ToDto(Order model);
    ICollection<Order> ToListModels(ICollection<OrderDto> dtos);
    ICollection<OrderDto> ToListDtos(ICollection<Order> models);
}