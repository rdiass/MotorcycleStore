using MotorcycleStore.Client.API.Settings;

namespace MotorcycleStore.Client.API.Configuration;

public static class MongoDbConfiguration
{
    public static IServiceCollection AddMongoDbConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<DeliveryManDatabaseSettings>(configuration.GetSection("DeliveryManDatabase"));

        return services;
    }
}
