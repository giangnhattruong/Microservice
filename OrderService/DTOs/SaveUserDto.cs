using System.ComponentModel.DataAnnotations;

namespace OrderService.DTOs;

public class SaveUserDto : BaseDto
{
    public string Id { get; set; }
    
    public string Name { get; set; }

    public SaveUserDto(string id, string name)
    {
        Id = id;
        Name = name;
    }
}