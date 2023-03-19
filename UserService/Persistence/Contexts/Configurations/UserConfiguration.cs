using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Models;

namespace UserService.Persistence.Contexts.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("AspNetUsers");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.FullName).IsRequired(false).HasMaxLength(255);
        
        builder.HasData(
            new User
            {
                Id = Guid.NewGuid().ToString(), 
                FullName = "James", 
                UserName = "james@example.com", 
                NormalizedUserName = "JAMES@EXAMPLE.COM",
                Email = "james@example.com", 
                NormalizedEmail = "JAMES@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "123456a@"),
            },
            new User
            {
                Id = Guid.NewGuid().ToString(), 
                FullName = "Steve", 
                UserName = "steve@example.com", 
                NormalizedUserName = "STEVE@EXAMPLE.COM",
                Email = "steve@example.com", 
                NormalizedEmail = "STEVE@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "123456a@"),
            },
            new User
            {
                Id = Guid.NewGuid().ToString(), 
                FullName = "Michael", 
                UserName = "michael@example.com", 
                NormalizedUserName = "MICHAEL@EXAMPLE.COM",
                Email = "michael@example.com", 
                NormalizedEmail = "MICHAEL@EXAMPLE.COM",
                EmailConfirmed = true,
                PasswordHash = new PasswordHasher<User>().HashPassword(null, "123456a@"),
            }
        );
    }
}