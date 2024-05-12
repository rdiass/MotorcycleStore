using Microsoft.AspNetCore.Http.Json;
using MotorcycleStore.WebAPI.Core.Identity;

namespace MotorcycleStore.Client.API.Configuration;

public static class ApiConfig
{
    public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
        });
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