namespace UserService.Startup;

public static class AppConfiguration
{
    public static WebApplication Configure(this WebApplication app)
    {
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger(options =>
            {
                options.RouteTemplate = app.Configuration["SwaggerOptions:JsonRoute"];
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(app.Configuration["SwaggerOptions:UIEndpoint"], app.Configuration["SwaggerOptions:Description"]);
            });
        }

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        return app;
    }
}