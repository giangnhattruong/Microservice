using OrderService.Domain.Models;

namespace OrderService.Domain.Repositories;

public interface IOrderRepository
{
    Task<IList<Order>?> ListAsync();
    Task AddAsync(Order order);
    Task<Order?> GetAsync(int id);
    void Update(Order order);
    void Remove(Order order);
}