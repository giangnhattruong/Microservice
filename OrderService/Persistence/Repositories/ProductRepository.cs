using Microsoft.EntityFrameworkCore;
using OrderService.Domain.Models;
using OrderService.Domain.Repositories;
using OrderService.Persistence.Contexts;

namespace OrderService.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;
    
    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<IList<Product>?> ListAsync()
    {
        return await _context.Products.AsNoTracking().ToListAsync();
    }
    
    public async Task AddAsync(Product user)
    {
        await _context.Products.AddAsync(user);
    }
    
    public async Task<Product?> GetAsync(int id)
    {
        return await _context.Products.FindAsync(id);
    }
    
    public void Update(Product order)
    {
        _context.Products.Update(order);
    }

    public void Remove(Product order)
    {
        _context.Products.Remove(order);
    }
}