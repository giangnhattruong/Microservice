using OrderService.Domain.Mapper;
using OrderService.Domain.Repositories;
using OrderService.Domain.Services;
using OrderService.Mappers;
using OrderService.Persistence.Repositories;
using OrderService.Services;

namespace OrderService.Startup;

public static class DependencyInjection
{
    public static IServiceCollection RegisteredServices(this IServiceCollection services)
    {
        // Add services to the container.

        services.AddControllers();
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
        services.AddScoped<IUserService, UserService>();

        services.AddScoped<IOrderDetailMapper, OrderDetailMapper>();
        services.AddScoped<IOrderMapper, OrderMapper>();
        services.AddScoped<IProductMapper, ProductMapper>();
        services.AddScoped<IUserMapper, UserMapper>();
        services.AddScoped<IUserOrderMapper, UserOrderMapper>();
        services.AddScoped<ISaveOrderDetailMapper, SaveOrderDetailMapper>();
        services.AddScoped<ISaveOrderMapper, SaveOrderMapper>();
        services.AddScoped<ISaveProductMapper, SaveProductMapper>();
        services.AddScoped<ISaveUserMapper, SaveUserMapper>();

        return services;
    }
}