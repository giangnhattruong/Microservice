using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Models;

namespace UserService.Persistence.Contexts.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(u => u.Id);
        builder.Property(u => u.Id).IsRequired().ValueGeneratedOnAdd();
        builder.Property(u => u.Name).IsRequired().HasMaxLength(255);
        
        builder.HasData
        (
            new {Id = 1, Name = "James"},
            new {Id = 2, Name = "Steve"},
            new {Id = 3, Name = "Michael"}
        );
    }
}