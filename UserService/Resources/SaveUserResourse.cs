using System.ComponentModel.DataAnnotations;

namespace UserService.Resources;

public class SaveUserResourse
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Password { get; set; }
}