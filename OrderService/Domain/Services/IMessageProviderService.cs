namespace OrderService.Domain.Services;

public interface IMessageProviderService<T>
{
    void SendMessage(string queue, T message);
}