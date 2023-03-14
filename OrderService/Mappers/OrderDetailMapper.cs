using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class OrderDetailMapper : IOrderDetailMapper
{
    public OrderDetail ToModel(OrderDetailDto dto)
    {
        var model = new OrderDetail();
        model.Product = dto.Product;
        model.Quantity = dto.Quantity;

        return model;
    }
    
    public OrderDetailDto ToDto(OrderDetail model)
    {
        return new OrderDetailDto(model.Product, model.Quantity);
    }
    
    public ICollection<OrderDetail> ToListModels(ICollection<OrderDetailDto> dtos)
    {
        return dtos.Select(dto => ToModel(dto)).ToList();
    }
    
    public ICollection<OrderDetailDto> ToListDtos(ICollection<OrderDetail> models)
    {
        return models.Select(model => ToDto(model)).ToList();
    }
}