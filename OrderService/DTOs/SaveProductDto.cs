namespace OrderService.DTOs;

public class SaveProductDto : BaseDto
{
    public string Name { get; set; }
    
    public decimal Price { get; set; }
    
    public SaveProductDto(string name, decimal price)
    {
        Name = name;
        Price = price;
    }
}