using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OrderService.Domain.Models;

namespace OrderService.Persistence.Contexts.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).IsRequired();
        builder.Property(p => p.FullName).IsRequired().HasMaxLength(255);

        builder.HasMany(u => u.Orders)
            .WithOne(o => o.User)
            .HasForeignKey(o => o.UserId);

        builder.HasData
        (
            new {Id = Guid.NewGuid().ToString(), FullName = "James"},
            new {Id = Guid.NewGuid().ToString(), FullName = "Steve"},
            new {Id = Guid.NewGuid().ToString(), FullName = "Michael"}
        );
    }
}