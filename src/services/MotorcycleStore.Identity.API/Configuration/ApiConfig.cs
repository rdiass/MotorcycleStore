using MotorcycleStore.Identity.API.Helpers;
using MotorcycleStore.WebAPI.Core.Identity;

namespace MotorcycleStore.Identity.API.Configuration;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddTransient<ITokenGenerator, TokenGenerator>();
        return services;
    }

    public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app)
    {
        app.UseHttpsRedirection();
        app.UseRouting();

        app.UseAuthConfiguration();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        return app;
    }
}
