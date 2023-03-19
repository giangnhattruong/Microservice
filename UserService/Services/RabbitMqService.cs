using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UserService.Domain.Services;

namespace UserService.Services;

public class RabbitMqService<T> : IMessageProviderService<T>, IMessageConsumerService<T>
{
    public IConfiguration _configuration;

    public RabbitMqService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void SendMessage(string queue, T message)
    {
        var channel = CreateChannel();
        
        channel.QueueDeclare(
            queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

        channel.BasicPublish(exchange: string.Empty,
            routingKey: queue,
            basicProperties: null,
            body: body);
    }

    public T? ReceiveMessage(string queue)
    {
        var channel = CreateChannel();
        
        channel.QueueDeclare(
            queue: queue,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var consumer = new EventingBasicConsumer(channel);

        T? objectResult = default(T);
        
        consumer.Received += (sender, args) =>
        {
            var message = Encoding.UTF8.GetString(args.Body.ToArray());
            objectResult = JsonConvert.DeserializeObject<T>(message);
        };
        
        channel.BasicConsume(queue: queue,
            autoAck: true,
            consumer: consumer);

        return objectResult;
    }

    private IModel CreateChannel()
    {
        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMq:Host"],
            UserName = _configuration["RabbitMq:UserName"],
            Password = _configuration["RabbitMq:Password"]
        };
        var connection = factory.CreateConnection();
        return connection.CreateModel();
    }
}