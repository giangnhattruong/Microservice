using ProductService.Domain.Models;

namespace ProductService.Domain.Repositories;

public interface ICategoryRepository
{
    Task<ICollection<Category>> ListAsync();
    Task AddAsync(Category category);
    Task<Category> FindByIdAsync(int id);
    void Update(Category category);
    void Remove(Category category);
}