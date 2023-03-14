using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Models;
using OrderService.Domain.Repositories;
using OrderService.Persistence.Contexts;

namespace OrderService.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    
    public UserRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IList<User>?> ListAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .Include(u => u.Orders)
            .ThenInclude(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ToListAsync();
    }
    
    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }
    
    public async Task<User?> GetAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Orders)
            .ThenInclude(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(u => u.Id == id);
    }
    
    public void Update(User order)
    {
        _context.Users.Update(order);
    }

    public void Remove(User order)
    {
        _context.Users.Remove(order);
    }
}