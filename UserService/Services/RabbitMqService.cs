using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserService.Domain.Services;

namespace UserService.Services;

public class RabbitMqService : IMessageBrokerService
{
    private readonly IConfiguration _configuration;

    public RabbitMqService(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public IConnection CreateChannel()
    {
        var connection = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMq:Host"],
            UserName = _configuration["RabbitMq:UserName"],
            Password = _configuration["RabbitMq:Password"]
        };
        connection.DispatchConsumersAsync = true;
        var channel = connection.CreateConnection();
        return channel;
    }
}