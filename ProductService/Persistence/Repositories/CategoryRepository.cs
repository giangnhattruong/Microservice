using Microsoft.EntityFrameworkCore;
using ProductService.Domain.Models;
using ProductService.Domain.Repositories;
using ProductService.Persistence.Contexts;

namespace ProductService.Persistence.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<ICollection<Category>> ListAsync()
    {
        return await _context.Categories.ToListAsync();
    }
    
    public async Task AddAsync(Category category) 
    {
        await _context.Categories.AddAsync(category);
    }
    
    public async Task<Category?> FindByIdAsync(int id)
    {
        return await _context.Categories.FindAsync(id);
    }
    
    public void Update(Category category)
    {
        _context.Categories.Update(category);
    }
    
    public void Remove(Category category)
    {
        _context.Categories.Remove(category);
    }
    
}