using OrderService.Domain.Models;
using OrderService.Domain.Repositories;
using OrderService.Persistence.Contexts;

namespace OrderService.Persistence.Repositories;

public class OrderDetailRepository : IOrderDetailRepository
{
    private readonly AppDbContext _context;
    
    public OrderDetailRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(OrderDetail orderDetail)
    {
        await _context.OrderDetails.AddAsync(orderDetail);
    }
    
    public void Remove(OrderDetail orderDetail)
    {
        _context.OrderDetails.Remove(orderDetail);
    }
}