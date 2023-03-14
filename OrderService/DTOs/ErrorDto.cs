namespace OrderService.DTOs;

public class ErrorDto
{
    public bool Success => false;
    public List<string> Messages { get; private set; }

    public ErrorDto(List<string> messages)
    {
        this.Messages = messages ?? new List<string>();
    }

    public ErrorDto(string message)
    {
        this.Messages = new List<string>();

        if (!string.IsNullOrWhiteSpace(message))
        {
            this.Messages.Add(message);
        }
    }
}