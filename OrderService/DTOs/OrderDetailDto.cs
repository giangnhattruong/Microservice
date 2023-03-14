using OrderService.Domain.Models;

namespace OrderService.DTOs;

public class OrderDetailDto : BaseDto
{
    public Product Product { get; set; }
    public int Quantity { get; set; }

    public OrderDetailDto(Product product, int quantity)
    {
        Product = product;
        Quantity = quantity;
    }
}