namespace OrderService.Domain.Mapper;

public interface IDtoToModelMapper<TDto, TModel>
{
    TModel ToModel(TDto dto);
    ICollection<TModel> ToListModels(ICollection<TDto> dtos);
}