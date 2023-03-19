using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class OrderDetailMapper : IModelToDtoMapper<OrderDetail, OrderDetailDto>
{
    private readonly IModelToDtoMapper<Product, ProductDto> _productMapper;

    public OrderDetailMapper(IModelToDtoMapper<Product, ProductDto> productMapper)
    {
        _productMapper = productMapper;
    }
    
    public OrderDetailDto? ToDto(OrderDetail? model)
    {
        return (model != null) ? new OrderDetailDto(_productMapper.ToDto(model.Product), model.Quantity) : null;
    }
    
    public ICollection<OrderDetailDto> ToListDtos(ICollection<OrderDetail>? models)
    {
        return models?.Select(model => ToDto(model)).ToList() ?? new List<OrderDetailDto>();
    }
}