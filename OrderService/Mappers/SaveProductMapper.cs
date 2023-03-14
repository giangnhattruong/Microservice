using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class SaveProductMapper : ISaveProductMapper
{
    public Product ToModel(SaveProductDto dto)
    {
        var model = new Product();
        model.Name = dto.Name;
        model.Price = dto.Price;

        return model;
    }
}