using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface ISaveUserMapper
{
    User ToModel(SaveUserDto dto);
}