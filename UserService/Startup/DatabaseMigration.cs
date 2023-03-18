using Microsoft.EntityFrameworkCore;
using UserService.Persistence.Contexts;

namespace UserService.Startup;

public static class DatabaseMigration
{
    public static WebApplication MigrateDatabase(this WebApplication app)
    {
        var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<AppDbContext>();
            context.Database.EnsureCreated();
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