using System.ComponentModel.DataAnnotations;

namespace OrderService.DTOs;

public class SaveOrderDetailDto : BaseDto
{
    [Required]
    public int ProductId { get; set; }
    
    [Required]
    public int Quantity { get; set; }

    public SaveOrderDetailDto(int productId, int quantity)
    {
        ProductId = productId;
        Quantity = quantity;
    }
}