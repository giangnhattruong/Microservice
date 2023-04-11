using OrderService.Controllers.Config;
using OrderService.Domain.Mapper;
using OrderService.Domain.Models;
using OrderService.Domain.Repositories;
using OrderService.Domain.Services;
using OrderService.DTOs;
using OrderService.Mappers;
using OrderService.Persistence.Repositories;
using OrderService.Services;

namespace OrderService.Startup;

public static class DependencyInjection
{
    public static IServiceCollection RegisteredServices(this IServiceCollection services)
    {
        var configuration = services.BuildServiceProvider().GetService<IConfiguration>();
        // Add services to the container.

        services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
        });
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IUserRepository, UserRepository>();        
        
        services.AddScoped<IOrderService, Services.OrderService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IUserService, Services.UserService>();
        services.AddSingleton<IMessageBrokerService, RabbitMqService>();
        services.AddScoped<IConsumerService, ConsumerService>();
        services.AddHostedService<ConsumerHostedService>();

        services.AddScoped<IModelToDtoMapper<OrderDetail, OrderDetailDto>, OrderDetailMapper>();
        services.AddScoped<IModelToDtoMapper<Order, OrderDto>, OrderMapper>();
        services.AddScoped<IModelToDtoMapper<Product, ProductDto>, ProductMapper>();
        services.AddScoped<IModelToDtoMapper<User, GeneralUserDto>, GeneralUserMapper>();
        services.AddScoped<IModelToDtoMapper<User, UserDto>, UserMapper>();
        services.AddScoped<IModelToDtoMapper<Order, UserOrderDto>, UserOrderMapper>();
        services.AddScoped<IDtoToModelMapper<SaveOrderDetailDto, OrderDetail>, SaveOrderDetailMapper>();
        services.AddScoped<IDtoToModelMapper<SaveOrderDto, Order>, SaveOrderMapper>();
        services.AddScoped<IDtoToModelMapper<SaveProductDto, Product>, SaveProductMapper>();
        services.AddScoped<IDtoToModelMapper<SaveUserDto, User>, SaveUserMapper>();

        return services;
    }
}