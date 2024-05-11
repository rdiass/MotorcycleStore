using MotorcycleStore.Client.API.Data;
using MotorcycleStore.Client.API.Services;

namespace MotorcycleStore.Client.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();
        services.AddScoped<DeliveryManService>();
    }
}
