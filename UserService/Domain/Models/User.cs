using Microsoft.AspNetCore.Identity;

namespace UserService.Domain.Models;

public class User : IdentityUser
{
    public string? FullName { get; set; }
}