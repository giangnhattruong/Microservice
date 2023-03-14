using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface ISaveProductMapper
{
    Product ToModel(SaveProductDto dto);
}