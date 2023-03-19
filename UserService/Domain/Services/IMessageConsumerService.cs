namespace UserService.Domain.Services;

public interface IMessageConsumerService<T>
{
    T? ReceiveMessage(string queue);
}