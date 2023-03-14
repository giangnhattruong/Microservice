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
    
    public User ToModel(UserDto dto)
    {
        var model = new User();
        model.Id = dto.Id;
        model.Name = dto.Name;
        model.Orders = _userOrderMapper.ToListModels(dto.Orders);

        return model;
    }
    
    public UserDto ToDto(User model)
    {
        return new UserDto(model.Id, model.Name, _userOrderMapper.ToListDtos(model.Orders));
    }
    
    public ICollection<User> ToListModels(ICollection<UserDto> dtos)
    {
        return dtos.Select(dto => ToModel(dto)).ToList();
    }
    
    public ICollection<UserDto> ToListDtos(ICollection<User> models)
    {
        return models.Select(model => ToDto(model)).ToList();
    }
}