using UserService.Domain.Models;

namespace UserService.Domain.Repositories;

public interface IUserRepository
{
    Task<ICollection<User>> ListAsync();

    Task RegisterAsync(User user, string password);

    Task<User?> FindAsync(string userName, string password);
    
    Task<User?> FindAsync(string userName);
    
    Task<User?> FindByEmailAsync(string email);
}