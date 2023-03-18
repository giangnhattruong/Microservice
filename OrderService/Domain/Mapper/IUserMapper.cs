using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface IUserMapper
{
    UserDto ToDto(User model);
    ICollection<UserDto> ToListDtos(ICollection<User> models);
}