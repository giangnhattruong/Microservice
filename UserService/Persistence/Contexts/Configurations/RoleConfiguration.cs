using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Models;

namespace UserService.Persistence.Contexts.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.HasMany(r => r.Users)
            .WithMany(u => u.Roles);
        
        builder.HasData(
            new Role {Id = Guid.NewGuid().ToString(), Name = "Admin"},
            new Role {Id = Guid.NewGuid().ToString(), Name = "Moderation"},
            new Role {Id = Guid.NewGuid().ToString(), Name = "Seller"},
            new Role {Id = Guid.NewGuid().ToString(), Name = "Customer"});
    }
}