using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface IProductMapper
{
    ProductDto ToDto(Product model);
    ICollection<ProductDto> ToListDtos(ICollection<Product> models);
}