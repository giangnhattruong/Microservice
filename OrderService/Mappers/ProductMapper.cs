using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class ProductMapper : IModelToDtoMapper<Product, ProductDto>
{
    public ProductDto? ToDto(Product? model)
    {
        return (model != null) ? new ProductDto(model.Id, model.Name, model.Price) : null;
    }
    
    public ICollection<ProductDto> ToListDtos(ICollection<Product>? models)
    {
        return models?.Select(model => ToDto(model)).ToList() ?? new List<ProductDto>();
    }
}