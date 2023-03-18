using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class ProductMapper : IProductMapper
{
    public ProductDto ToDto(Product model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        
        return new ProductDto(model.Id, model.Name, model.Price);
    }
    
    public ICollection<ProductDto> ToListDtos(ICollection<Product> models)
    {
        return models.Select(model => ToDto(model)).ToList();
    }
}