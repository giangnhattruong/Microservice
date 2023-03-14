using OrderService.Domain.Models;

namespace OrderService.Domain.Repositories;

public interface IOrderDetailRepository
{
    Task AddAsync(OrderDetail orderDetail);
    void Remove(OrderDetail orderDetail);
}