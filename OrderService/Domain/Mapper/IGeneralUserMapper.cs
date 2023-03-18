using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface IGeneralUserMapper
{
    GeneralUserDto ToDto(User model);
    ICollection<GeneralUserDto> ToListDtos(ICollection<User> models);
}