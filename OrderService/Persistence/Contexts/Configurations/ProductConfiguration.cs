using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Models;

namespace OrderService.Persistence.Contexts.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(p => p.Name).IsRequired().HasMaxLength(255);
        builder.Property(p => p.Price).HasColumnType("decimal(18,2)").IsRequired();

        builder.HasMany(p => p.OrderDetails)
            .WithOne(od => od.Product)
            .HasForeignKey(od => od.ProductId);

        builder.HasData
        (
            new {Id = 1, Name = "Apple", Price = 3.20M},
            new {Id = 2, Name = "Orange", Price = 5.70M},
            new {Id = 3, Name = "Banana", Price = 8.45M}
        );
    }
}