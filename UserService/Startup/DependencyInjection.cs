using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using UserService.Controllers.Config;
using UserService.Domain.Models;
using UserService.Domain.Repositories;
using UserService.Domain.Services;
using UserService.Persistence.Contexts;
using UserService.Persistence.Repositories;
using UserService.Services;

namespace UserService.Startup;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
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

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped<IUserService, Services.UserService>();
        services.AddScoped<ITokenService, JwtService>();
        services.AddScoped<IMessageProviderService<User>, RabbitMqService<User>>();

        return services;
    }
}