using Newtonsoft.Json;
using OrderService.Domain.Models;
using OrderService.Domain.Repositories;
using OrderService.Domain.Services;
using OrderService.DTOs;
using OrderService.Resources;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace OrderService.Services;

public class ConsumerService : IConsumerService
{
    private readonly IModel _model;
    
    private readonly IConnection _connection;
    
    private const string QueueName = "user";

    private readonly IUserService _userService;

    private readonly IProductService _productService;
    
    private readonly IUserRepository _userRepository;
    
    private readonly IUnitOfWork _unitOfWork;

    public ConsumerService(
        IMessageBrokerService mqService, 
        IUserService userService,
        IProductService productService,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _userService = userService;
        _productService = productService;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _connection = mqService.CreateChannel();
        _model = _connection.CreateModel();
        _model.QueueDeclare(QueueName, durable: true, exclusive: false, autoDelete: false);
        _model.ExchangeDeclare("UserExchange", ExchangeType.Fanout, durable: true, autoDelete: false);
        _model.QueueBind(QueueName, "UserExchange", string.Empty);
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
            var user = JsonConvert.DeserializeObject<GeneralUserResource>(text);
            await CreateUser(user);
            
            await Task.CompletedTask;
            _model.BasicAck(ea.DeliveryTag, false);
        };
        _model.BasicConsume(QueueName, false, consumer);
        await Task.CompletedTask;

    }

    private async Task CreateUser(GeneralUserResource resource)
    {
        // await _userService.AddAsync(userDto);
        await _userRepository.AddAsync(new User() { Id = resource.Id, FullName = resource.FullName });
        await _unitOfWork.CompleteAsync();
    }

    private async Task CreateProduct(SaveProductDto productDto)
    {
        await _productService.AddAsync(productDto);
    }

    public void Dispose()
    {
        if (_model.IsOpen)
            _model.Close();
        if (_connection.IsOpen)
            _connection.Close();
    }
}