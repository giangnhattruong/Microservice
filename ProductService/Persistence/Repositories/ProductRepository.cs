using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Models;
using ProductService.Domain.Repositories;
using ProductService.Persistence.Contexts;

namespace ProductService.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly AppDbContext _context;

    public ProductRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Product>> ListAsync()
    {
        return await _context.Products
                            .Include(p => p.Category)
                            .ToListAsync();
    }
    
    public async Task AddAsync(Product product)
    {
        await _context.Products.AddAsync(product);
    }
    
    public async Task<Product?> FindByIdAsync(int id)
    {
        return await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public void Update(Product product)
    {
        _context.Products.Update(product);
    }
    
    public void Remove(Product product)
    {
        _context.Products.Remove(product);
    }
    
}