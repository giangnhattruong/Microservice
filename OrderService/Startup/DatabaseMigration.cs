using Microsoft.EntityFrameworkCore;
using OrderService.Persistence.Contexts;

namespace OrderService.Startup;

public static class DatabaseMigration
{
    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.Migrate();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occured during Seeding Data");
        }

        return app;
    }
}