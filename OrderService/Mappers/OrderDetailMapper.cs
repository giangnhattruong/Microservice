using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class OrderDetailMapper : IOrderDetailMapper
{
    private readonly IProductMapper _productMapper;

    public OrderDetailMapper(IProductMapper productMapper)
    {
        _productMapper = productMapper;
    }
    
    public OrderDetailDto ToDto(OrderDetail model)
    {
        return new OrderDetailDto(_productMapper.ToDto(model.Product), model.Quantity);
    }
    
    public ICollection<OrderDetailDto> ToListDtos(ICollection<OrderDetail> models)
    {
        return models.Select(model => ToDto(model)).ToList();
    }
}