using OrderService.DTOs;

namespace OrderService.Domain.Mapper;

public interface IModelToDtoMapper<TModel, TDto>
{
    TDto? ToDto(TModel? model);
    ICollection<TDto> ToListDtos(ICollection<TModel>? models);
}