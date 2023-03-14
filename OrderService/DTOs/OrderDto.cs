namespace OrderService.DTOs;

public class OrderDto : BaseDto
{
    public int Id { get; set; }
    public UserDto User { get; set; }
    public ICollection<OrderDetailDto> Products { get; set; }
    public decimal TotalAmount => Products.Sum(op => op.Quantity * op.Product.Price);
    public DateTime CreatedAt { get; set; }

    public OrderDto(int id, UserDto user, ICollection<OrderDetailDto> products, DateTime createdAt)
    {
        Id = id;
        User = user;
        Products = products;
        CreatedAt = createdAt;
    }
    
    public OrderDto(int id, UserDto user, DateTime createdAt)
    {
        Id = id;
        User = user;
        Products = new List<OrderDetailDto>();
        CreatedAt = createdAt;
    }
}