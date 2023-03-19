namespace UserService.Resources;

public class UserResource
{
    public string? Id { get; set; }
    
    public string? FullName { get; set; }
    
    public string? UserName { get; set; }
    
    public string? Email { get; set; }

    public ICollection<RoleResource> Roles { get; set; } = new List<RoleResource>();
}