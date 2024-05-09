using MotorcycleStore.Catalog.API.Services;
using MotorcycleStore.Catalog.Settings;

namespace MotorcycleStore.Catalog.API.Configuration;

public static class MongoDbConfiguration
{
    public static IServiceCollection AddMongoDbConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CatalogDatabaseSettings>(configuration.GetSection("CatalogDatabase"));

        services.AddSingleton<MotorcycleService>();

        return services;
    }
}
