using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ProductService.Domain;
using ProductService.Domain.Models;

namespace ProductService.Persistence.Contexts;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}