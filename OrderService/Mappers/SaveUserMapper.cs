using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class SaveUserMapper : IDtoToModelMapper<SaveUserDto, User>
{
    public User ToModel(SaveUserDto dto)
    {
        if (dto == null)
        {
            throw new ArgumentNullException(nameof(dto));
        }

        return new User() {Name = dto.Name};
    }

    public ICollection<User> ToListModels(ICollection<SaveUserDto> dtos)
    {
        return dtos.Select(d => ToModel(d)).ToList();
    }
}