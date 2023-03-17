using System.ComponentModel.DataAnnotations;

namespace ProductService.Resources;

public class SaveProductResource
{
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }
    
    [Range(0, 10000)]
    public int? Stock { get; set; }
    
    [Range(0, 1000)]
    public decimal? Price { get; set; }
    
    [Required]
    public int CategoryId { get; set; }
}