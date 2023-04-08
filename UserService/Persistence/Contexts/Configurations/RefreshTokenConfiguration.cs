using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.Domain.Models;

namespace UserService.Persistence.Contexts.Configurations;

public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.ToTable("RefreshTokens");
        builder.HasKey(rt => rt.Token);
        builder.Property(rt => rt.JwtId).IsRequired();
        builder.Property(rt => rt.CreationDate).HasColumnType("timestamp").IsRequired();
        builder.Property(rt => rt.ExpiryDate).HasColumnType("timestamp").IsRequired();
        builder.Property(rt => rt.Used).HasDefaultValue(false);
        builder.Property(rt => rt.Invalidated).HasDefaultValue(false);

        builder.HasOne(rt => rt.User)
            .WithMany(u => u.RefreshTokens)
            .HasForeignKey(rt => rt.UserId);
    }
}