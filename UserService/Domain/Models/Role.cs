using Microsoft.AspNetCore.Identity;

namespace UserService.Domain.Models;

public class Role : IdentityRole
{
    public ICollection<User> Users { get; set; } = new List<User>();
}