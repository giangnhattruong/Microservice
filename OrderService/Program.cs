using Microsoft.EntityFrameworkCore;
using OrderService.Persistence.Contexts;
using OrderService.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add DB context
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("OrderServiceDatabase")));

// Add services
builder.Services.RegisteredServices();

var app = builder.Build();

// Configure app
app.Configure();

app.MigrateDatabase();

app.Run();
