using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class SaveProductMapper : IDtoToModelMapper<SaveProductDto, Product>
{
    public Product ToModel(SaveProductDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        return new Product()  {Name = dto.Name, Price = dto.Price};
    }

    public ICollection<Product> ToListModels(ICollection<SaveProductDto> dtos)
    {
        return dtos.Select(d => ToModel(d)).ToList();
    }
}