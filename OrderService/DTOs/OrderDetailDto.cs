using OrderService.Domain.Models;

namespace OrderService.DTOs;

public class OrderDetailDto : BaseDto
{
    public ProductDto? Product { get; set; }
    public int Quantity { get; set; }

    public OrderDetailDto(ProductDto? product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}