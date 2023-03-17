using ProductService.Domain.Models;
using ProductService.Services.Communication;

namespace ProductService.Domain.Services;

public interface IProductService
{
    Task<ICollection<Product>> ListAsync();

    Task<BaseResponse<Product>> FindByIdAsync(int id);

    Task<BaseResponse<Product>> AddAsync(Product product);

    Task<BaseResponse<Product>> UpdateAsync(int id, Product product);

    Task<BaseResponse<Product>> RemoveAsync(int id);
}