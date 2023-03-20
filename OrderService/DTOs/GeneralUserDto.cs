namespace OrderService.DTOs;

public class GeneralUserDto
{
    public string Id { get; set; }
    
    public string FullName { get; set; }
    
    public GeneralUserDto(string id, string name)
    {
        Id = id;
        FullName = name;
    }
}