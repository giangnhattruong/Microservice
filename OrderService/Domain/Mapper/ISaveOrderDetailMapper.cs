using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface ISaveOrderDetailMapper
{
    OrderDetail ToModel(SaveOrderDetailDto dto);
    ICollection<OrderDetail> ToListModels(ICollection<SaveOrderDetailDto> dtos);
}