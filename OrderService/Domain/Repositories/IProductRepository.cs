using OrderService.Domain.Models;

namespace OrderService.Domain.Repositories;

public interface IProductRepository
{
    Task<IList<Product>?> ListAsync();
    Task AddAsync(Product product);
    Task<Product?> GetAsync(int id);
    void Update(Product product);
    void Remove(Product product);
}