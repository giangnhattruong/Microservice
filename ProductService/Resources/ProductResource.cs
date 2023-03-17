namespace ProductService.Resources;

public class ProductResource
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public decimal? Price { get; set; }

    public int Stock { get; set; } = 0;
    
    public CategoryResource Category { get; set; }
}