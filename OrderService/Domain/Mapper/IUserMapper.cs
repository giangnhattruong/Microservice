using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface IUserMapper
{
    User ToModel(UserDto dto);
    UserDto ToDto(User model);
    ICollection<User> ToListModels(ICollection<UserDto> dtos);
    ICollection<UserDto> ToListDtos(ICollection<User> models);
}