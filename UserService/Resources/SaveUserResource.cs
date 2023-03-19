using System.ComponentModel.DataAnnotations;

namespace UserService.Resources;

public class SaveUserResource
{
    [MaxLength(255)]
    public string? FullName { get; set; }
    
    [Required]
    [MaxLength(60)]
    public string? UserName { get; set; }    
    
    [Required]
    [MaxLength(120)]
    public string? Email { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string? Password { get; set; }
}