namespace MotorcycleStore.Catalog.API.Configuration;

public static class SwaggerConfig
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new()
            {
                Version = "v1",
                Title = "MotorcycleStore Catalog API",
                Description = "This API it is used for create new users and login",
                Contact = new() { Name = "Rafael Santos", Email = "rafaeldias.a@hotmail.com" }
            });
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        });

        return app;
    }
}
