namespace OrderService.Domain.Services;

public interface IConsumerService
{
    Task ReadMessages();
}