using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface IOrderDetailMapper
{
    OrderDetail ToModel(OrderDetailDto dto);
    OrderDetailDto ToDto(OrderDetail model);
    ICollection<OrderDetail> ToListModels(ICollection<OrderDetailDto> dtos);
    ICollection<OrderDetailDto> ToListDtos(ICollection<OrderDetail> models);
}