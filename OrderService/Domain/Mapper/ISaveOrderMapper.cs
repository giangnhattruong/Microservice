using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface ISaveOrderMapper
{
    Order ToModel(SaveOrderDto dto);
}