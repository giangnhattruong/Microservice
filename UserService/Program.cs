using System.Text;
using Microsoft.EntityFrameworkCore;
using UserService.Persistence.Contexts;
using UserService.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add DB context
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("UserServiceDatabase")).EnableSensitiveDataLogging());

// Add services
builder.Services.RegisterServices();

var app = builder.Build();

// Configure app
app.Configure();

app.MigrateDatabase();

app.Run();