using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class GeneralUserMapper : IGeneralUserMapper
{
    public GeneralUserDto ToDto(User model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model));
        }

        return new GeneralUserDto(model.Id, model.Name);
    }

    public ICollection<GeneralUserDto> ToListDtos(ICollection<User> models)
    {
        return models.Select(m => ToDto(m)).ToList();
    }
}