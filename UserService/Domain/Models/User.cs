using Microsoft.AspNetCore.Identity;

namespace UserService.Domain.Models;

public class User : IdentityUser
{
    public string? FullName { get; set; }

    public virtual ICollection<Role> Roles { get; set; } = new List<Role>();

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}