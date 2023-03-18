using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface IOrderDetailMapper
{
    OrderDetailDto ToDto(OrderDetail model);
    ICollection<OrderDetailDto> ToListDtos(ICollection<OrderDetail> models);
}