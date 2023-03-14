using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Models;
using OrderService.Domain.Repositories;
using OrderService.Persistence.Contexts;

namespace OrderService.Persistence.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;
    
    public OrderRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IList<Order>?> ListAsync()
    {
        return await _context.Orders
            .AsNoTracking()
            .Include(o => o.User)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .ToListAsync();
    }
    
    public async Task AddAsync(Order user)
    {
        await _context.Orders.AddAsync(user);
    }
    
    public async Task<Order?> GetAsync(int id)
    {
        return await _context.Orders
            .Include(o => o.User)
            .Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product)
            .FirstOrDefaultAsync(od => od.Id == id);
    }
    
    public void Update(Order order)
    {
        _context.Orders.Update(order);
    }

    public void Remove(Order order)
    {
        _context.Orders.Remove(order);
    }
}