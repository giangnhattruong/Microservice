namespace OrderService.Domain.Models;

public class Order : BaseModel
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public User User { get; set; }
    public DateTime CreateAt { get; set; }
    public ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}