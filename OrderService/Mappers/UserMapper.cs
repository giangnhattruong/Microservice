using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class UserMapper : IUserMapper
{
    private readonly IUserOrderMapper _userOrderMapper;

    public UserMapper(IUserOrderMapper userOrderMapper)
    {
        _userOrderMapper = userOrderMapper;
    }
    
    public UserDto ToDto(User model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }
        
        return new UserDto(model.Id, model.Name, _userOrderMapper.ToListDtos(model.Orders));
    }
    
    public ICollection<UserDto> ToListDtos(ICollection<User> models)
    {
        return models.Select(model => ToDto(model)).ToList();
    }
}