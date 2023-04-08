using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Domain.Models;
using UserService.Domain.Repositories;

namespace UserService.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly UserManager<User> _userManager;

    public UserRepository(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<ICollection<User>> ListAsync()
    {
        return await _userManager.Users.AsNoTracking().ToListAsync();
    }

    public async Task RegisterAsync(User user, string password)
    {
        await _userManager.CreateAsync(user, password);
    }

    public async Task<User?> FindAsync(string userName, string password)
    {
        // Find user by UserName or Email

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName) ??
                        await _userManager.Users.FirstOrDefaultAsync(u => u.Email == userName);
        
        // Check if password matched
        var isPasswordMatched = (user != null) && await _userManager.CheckPasswordAsync(user, password);

        return isPasswordMatched ? user : null;
    }

    public async Task<User?> FindAsync(string userName)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == userName);
    }
    
    public async Task<User?> FindByEmailAsync(string email)
    {
        return await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}