using OrderService.Domain.Models;

namespace OrderService.Domain.Repositories;

public interface IUserRepository
{
    Task<IList<User>?> ListAsync();
    Task AddAsync(User user);
    Task<User?> GetAsync(string id);
    void Update(User user);
    void Remove(User user);
}