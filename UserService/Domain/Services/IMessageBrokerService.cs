using RabbitMQ.Client;

namespace UserService.Domain.Services;

public interface IMessageBrokerService
{
    IConnection CreateChannel();
}