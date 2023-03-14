using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class SaveOrderDetailMapper : ISaveOrderDetailMapper
{
    public OrderDetail ToModel(SaveOrderDetailDto dto)
    {
        var model = new OrderDetail();
        model.ProductId = dto.ProductId;
        model.Quantity = dto.Quantity;

        return model;
    }
    
    public ICollection<OrderDetail> ToListModels(ICollection<SaveOrderDetailDto> dtos)
    {
        return dtos.Select(dto => ToModel(dto)).ToList();
    }
}