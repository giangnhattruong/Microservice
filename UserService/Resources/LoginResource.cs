using System.ComponentModel.DataAnnotations;

namespace UserService.Resources;

public class LoginResource
{
    [Required]
    public string UserName { get; set; }
    
    [Required]
    public string Password { get; set; }
}