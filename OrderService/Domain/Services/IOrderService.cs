using OrderService.Domain.Models;
using OrderService.Services.Communication;
using OrderService.DTOs;

namespace OrderService.Domain.Services;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>?> ListAsync();
    Task<BaseResponse<OrderDto>> AddAsync(SaveOrderDto user);
    Task<BaseResponse<OrderDto>> UpdateAsync(int id, SaveOrderDto user);
    Task<BaseResponse<OrderDto>> RemoveAsync(int id);
}