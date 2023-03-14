using OrderService.Domain.Models;
using OrderService.DTOs;
using OrderService.Services.Communication;

namespace OrderService.Domain.Services;

public interface IProductService
{
    Task<IEnumerable<ProductDto>?> ListAsync();
    Task<BaseResponse<ProductDto>> AddAsync(SaveProductDto user);
    Task<BaseResponse<ProductDto>> UpdateAsync(int id, SaveProductDto user);
    Task<BaseResponse<ProductDto>> RemoveAsync(int id);
}