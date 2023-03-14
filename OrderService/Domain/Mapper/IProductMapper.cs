using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface IProductMapper
{
    Product ToModel(ProductDto dto);
    ProductDto ToDto(Product model);
    ICollection<Product> ToListModels(ICollection<ProductDto> dtos);
    ICollection<ProductDto> ToListDtos(ICollection<Product> models);
}