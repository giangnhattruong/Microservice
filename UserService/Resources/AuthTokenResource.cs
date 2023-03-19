namespace UserService.Resources;

public class AuthTokenResource
{
    public string Token { get; set; }
    
    public DateTime ExpiredAt { get; set; }
}