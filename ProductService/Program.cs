using Microsoft.EntityFrameworkCore;
using ProductService.Persistence.Contexts;
using ProductService.Startup;

var builder = WebApplication.CreateBuilder(args);

// Add DB context
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseNpgsql(builder.Configuration.GetConnectionString("ProductServiceDatabase")));

// Add services
builder.Services.RegisterServices();

var app = builder.Build();

// Configure app
app.Configure();

app.MigrateDatabase();

app.Run();