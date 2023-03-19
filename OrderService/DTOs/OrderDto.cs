namespace OrderService.DTOs;

public class OrderDto : BaseDto
{
    public int Id { get; set; }
    public GeneralUserDto? User { get; set; }
    public ICollection<OrderDetailDto> Products { get; set; }
    public decimal TotalAmount => Products?.Sum(op => op?.Quantity ?? 0 * op?.Product?.Price ?? 0) ?? 0;
    public DateTime CreatedAt { get; set; }

    public OrderDto(int id, GeneralUserDto? user, ICollection<OrderDetailDto> products, DateTime createdAt)
    {
        Id = id;
        User = user;
        Products = products;
        CreatedAt = createdAt;
    }
    
    public OrderDto(int id, GeneralUserDto? user, DateTime createdAt)
    {
        Id = id;
        User = user;
        Products = new List<OrderDetailDto>();
        CreatedAt = createdAt;
    }
}