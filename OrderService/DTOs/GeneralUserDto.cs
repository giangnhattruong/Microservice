namespace OrderService.DTOs;

public class GeneralUserDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public GeneralUserDto(int id, string name)
    {
        Id = id;
        Name = name;
    }
}