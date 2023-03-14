using System.ComponentModel.DataAnnotations;

namespace OrderService.DTOs;

public class SaveUserDto : BaseDto
{
    public string Name { get; set; }

    public SaveUserDto(string name)
    {
        Name = name;
    }
}