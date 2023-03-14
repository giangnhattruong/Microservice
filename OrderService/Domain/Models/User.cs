namespace OrderService.Domain.Models;

public class User : BaseModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Order> Orders { get; set; } = new List<Order>();
}