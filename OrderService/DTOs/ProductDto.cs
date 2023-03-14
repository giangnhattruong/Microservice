namespace OrderService.DTOs;

public class ProductDto : BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    
    public ProductDto(int id, string name, decimal price)
    {
        Id = id;
        Name = name;
        Price = price;
    }
}