using Microsoft.AspNetCore.Identity;
using UserService.Controllers.Config;
using UserService.Domain.Models;
using UserService.Domain.Repositories;
using UserService.Persistence.Contexts;
using UserService.Persistence.Repositories;

namespace UserService.Startup;

public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        // Add services to the container.

        services.AddControllers().ConfigureApiBehaviorOptions(options =>
        {
            options.InvalidModelStateResponseFactory = InvalidModelStateResponseFactory.ProduceErrorResponse;
        });
        
        // Add identity service
        services.AddIdentityCore<User>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.User.RequireUniqueEmail = true;
            options.Password.RequireDigit = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        }).AddEntityFrameworkStores<AppDbContext>();
        services.AddScoped<UserManager<User>>();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}