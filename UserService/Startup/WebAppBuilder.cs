namespace UserService.Startup;

public static class WebAppBuilder
{
    public static IHostBuilder ConfigureAppSettings(this IHostBuilder host)
    {
        host.ConfigureAppConfiguration((ctx, builder) =>
        {
            builder.AddJsonFile("appsettings.json", false, true);
            builder.AddEnvironmentVariables();
        });

        return host;
    }
}