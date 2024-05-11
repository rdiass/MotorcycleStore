using MotorcycleStore.Catalog.API.Data;
using MotorcycleStore.Catalog.API.Services;

namespace MotorcycleStore.Catalog.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
        services.AddScoped<MotorcycleService>();
    }
}
