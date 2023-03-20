namespace OrderService.DTOs;

public class UserDto : BaseDto
{
    public string Id { get; set; }
    public string FullName { get; set; }
    public ICollection<UserOrderDto> Orders { get; set; }
    
    public UserDto(string id, string name, ICollection<UserOrderDto> orders)
    {
        Id = id;
        FullName = name;
        Orders = orders;
    }

    public UserDto(string id, string name)
    {
        Id = id;
        FullName = name;
        Orders = new List<UserOrderDto>();
    }
}