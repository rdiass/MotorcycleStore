using MotorcycleStore.Catalog.Settings;

namespace MotorcycleStore.Catalog.API.Configuration;

public static class MongoDbConfiguration
{
    public static IServiceCollection AddMongoDbConfig(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<CatalogDatabaseSettings>(configuration.GetSection("CatalogDatabase"));

        return services;
    }
}
