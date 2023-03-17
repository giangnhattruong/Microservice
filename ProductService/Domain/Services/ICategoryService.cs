using ProductService.Domain.Models;
using ProductService.Services.Communication;

namespace ProductService.Domain.Services;

public interface ICategoryService
{
    Task<ICollection<Category>> ListAsync();

    Task<BaseResponse<Category>> FindByIdAsync(int id);

    Task<BaseResponse<Category>> AddAsync(Category category);

    Task<BaseResponse<Category>> UpdateAsync(int id, Category category);

    Task<BaseResponse<Category>> RemoveAsync(int id);
}