using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.DTOs;

namespace OrderService.Mappers;

public class GeneralUserMapper : IModelToDtoMapper<User, GeneralUserDto>
{
    public GeneralUserDto? ToDto(User? model)
    {
        return (model != null) ? new GeneralUserDto(model.Id, model.FullName) : null;
    }

    public ICollection<GeneralUserDto> ToListDtos(ICollection<User>? models)
    {
        return models?.Select(m => ToDto(m)).ToList() ?? new List<GeneralUserDto>();
    }
}