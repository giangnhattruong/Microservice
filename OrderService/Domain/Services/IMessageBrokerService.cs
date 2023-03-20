using RabbitMQ.Client;

namespace OrderService.Domain.Services;

public interface IMessageBrokerService
{
    IConnection CreateChannel();
}