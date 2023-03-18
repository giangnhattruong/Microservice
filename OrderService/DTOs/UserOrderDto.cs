namespace OrderService.DTOs;

public class UserOrderDto : BaseDto
{
    public int Id { get; set; }
    public ICollection<OrderDetailDto> Products { get; set; }
    public decimal TotalAmount => Products?.Sum(op => op.Quantity * op.Product.Price) ?? 0;
    public DateTime CreatedAt { get; set; }

    public UserOrderDto(int id, ICollection<OrderDetailDto> products, DateTime createdAt)
    {
        Id = id;
        Products = products;
        CreatedAt = createdAt;
    }

    public UserOrderDto(int id, DateTime createdAt)
    {
        Id = id;
        Products = new List<OrderDetailDto>();
        CreatedAt = createdAt;
    }
}