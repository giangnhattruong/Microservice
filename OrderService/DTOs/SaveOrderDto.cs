using System.ComponentModel.DataAnnotations;

namespace OrderService.DTOs;

public class SaveOrderDto : BaseDto
{
    [Required]
    public string UserId { get; set; }
    
    [Required]
    public ICollection<SaveOrderDetailDto> Products { get; set; }
    
    public SaveOrderDto(string userId, ICollection<SaveOrderDetailDto> products)
    {
        UserId = userId;
        Products = products;
    }
}
