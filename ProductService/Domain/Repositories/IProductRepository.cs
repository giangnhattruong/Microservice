using ProductService.Domain.Models;

namespace ProductService.Domain.Repositories;

public interface IProductRepository
{
    Task<ICollection<Product>> ListAsync();
    Task AddAsync(Product product);
    Task<Product> FindByIdAsync(int id);
    void Update(Product product);
    void Remove(Product product);
}