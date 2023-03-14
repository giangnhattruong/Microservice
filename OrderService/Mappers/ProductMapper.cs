using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class ProductMapper : IProductMapper
{
    public Product ToModel(ProductDto dto)
    {
        var model = new Product();
        model.Id = dto.Id;
        model.Name = dto.Name;
        model.Price = dto.Price;

        return model;
    }
    
    public ProductDto ToDto(Product model)
    {
        return new ProductDto(model.Id, model.Name, model.Price);
    }
    
    public ICollection<Product> ToListModels(ICollection<ProductDto> dtos)
    {
        return dtos.Select(dto => ToModel(dto)).ToList();
    }
    
    public ICollection<ProductDto> ToListDtos(ICollection<Product> models)
    {
        return models.Select(model => ToDto(model)).ToList();
    }
}