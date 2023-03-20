using Newtonsoft.Json;
using OrderService.Domain.Services;
using OrderService.DTOs;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderService.Services;

public class ConsumerService : IConsumerService
{
    private readonly IModel _model;
    
    private readonly IConnection _connection;
    
    private const string _queueName = "user";

    private readonly IUserService _userService;

    public ConsumerService(IMessageBrokerService mqService, IUserService userService)
    {
        _userService = userService;
        _connection = mqService.CreateChannel();
        _model = _connection.CreateModel();
        _model.QueueDeclare(_queueName, durable: true, exclusive: false, autoDelete: false);
        _model.ExchangeDeclare("UserExchange", ExchangeType.Fanout, durable: true, autoDelete: false);
        _model.QueueBind(_queueName, "UserExchange", string.Empty);
    }
    
    public async Task ReadMessages()
    {
        var consumer = new AsyncEventingBasicConsumer(_model);
        string text = null;
        consumer.Received += async (ch, ea) =>
        {
            var body = ea.Body.ToArray();
            text = System.Text.Encoding.UTF8.GetString(body);
            Console.WriteLine(text);
            await Task.CompletedTask;
            _model.BasicAck(ea.DeliveryTag, false);
        };
        _model.BasicConsume(_queueName, false, consumer);
        await Task.CompletedTask;
        // var user = JsonConvert.DeserializeObject<SaveUserDto>(text);
    }

    public void Dispose()
    {
        if (_model.IsOpen)
            _model.Close();
        if (_connection.IsOpen)
            _connection.Close();
    }
}