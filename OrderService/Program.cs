using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using OrderService.Persistence.Contexts;
using OrderService.Startup;
using SetupBuilderExtensions = NLog.SetupBuilderExtensions;

var logger = SetupBuilderExtensions.GetCurrentClassLogger(NLog.LogManager.Setup().LoadConfigurationFromAppSettings());
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add DB context
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("OrderServiceDatabase")));

    // Add services
    builder.Services.RegisteredServices();

    // Add Authentication
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer("Bearer", options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidAudience = builder.Configuration["Jwt:Audience"],
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])
                )
            };
        });

    // Add Authorization
    builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("RequireAdminRole", policy =>
        {
            policy.RequireAuthenticatedUser();
            policy.RequireRole("admin");
        });
    });

    // NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var app = builder.Build();

    AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

    // Configure app
    app.Configure();

    app.MigrateDatabase();

    app.Run();

}
catch (Exception ex)
{
    // NLog: catch setup errors
    logger.Error(ex, "Stopped program because of exception");
    throw;
}
finally
{
    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
    NLog.LogManager.Shutdown();
}