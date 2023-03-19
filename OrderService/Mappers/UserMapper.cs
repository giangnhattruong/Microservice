using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class UserMapper : IModelToDtoMapper<User, UserDto>
{
    private readonly IModelToDtoMapper<Order, UserOrderDto> _userOrderMapper;

    public UserMapper(IModelToDtoMapper<Order, UserOrderDto> userOrderMapper)
    {
        _userOrderMapper = userOrderMapper;
    }
    
    public UserDto? ToDto(User? model)
    {
        return (model != null) ? new UserDto(model.Id, model.Name, _userOrderMapper.ToListDtos(model.Orders)) : null;
    }
    
    public ICollection<UserDto> ToListDtos(ICollection<User>? models)
    {
        return models?.Select(model => ToDto(model)).ToList() ?? new List<UserDto>();
    }
}