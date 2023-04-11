using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using UserService.Domain.Models;
using UserService.Options;
using UserService.Persistence.Contexts;
using UserService.Startup;
using SetupBuilderExtensions = NLog.SetupBuilderExtensions;

var logger = SetupBuilderExtensions.GetCurrentClassLogger(NLog.LogManager.Setup().LoadConfigurationFromAppSettings());
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.ConfigureAppSettings();
    
    // Add DB context
    builder.Services.AddDbContext<AppDbContext>(opt =>
        opt.UseNpgsql(builder.Configuration.GetConnectionString("UserServiceDatabase")).EnableSensitiveDataLogging());
        // builder.Services.AddDbContext<AppDbContext>(o => o.UseInMemoryDatabase("TestDb"));
        
        
    // Add services
    builder.Services.RegisterServices();

    // Add identity service
    builder.Services.AddIdentityCore<User>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
    }).AddRoles<IdentityRole>().AddEntityFrameworkStores<AppDbContext>();
    builder.Services.AddScoped<UserManager<User>>();

    var jwtSettings = builder.Configuration.GetRequiredSection("JwtSettings").Get<JwtSettings>();
    builder.Services.AddSingleton(jwtSettings);

    // Add Authentication
    builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer("Bearer", options =>
        {
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.Zero,
                ValidateIssuerSigningKey = true,
                ValidAudience = jwtSettings.Audience,
                ValidIssuer = jwtSettings.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(jwtSettings.Secret)
                )
            };;
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

    var app = builder.Build();

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