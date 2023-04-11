namespace UserService.Contracts.V1.Response;

public class ErrorModel
{
    public string Field { get; set; }

    public List<string> Messages { get; set; } = new List<string>();
}