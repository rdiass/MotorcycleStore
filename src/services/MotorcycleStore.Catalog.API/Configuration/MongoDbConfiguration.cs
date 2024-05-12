using Microsoft.EntityFrameworkCore;
using MotorcycleStore.Catalog.API.Data;
using MotorcycleStore.Catalog.Settings;

namespace MotorcycleStore.Catalog.API.Configuration;

public static class MongoDbConfiguration
{
    public static IServiceCollection AddMongoDbConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDBSettings = configuration.GetSection("CatalogDatabase").Get<CatalogDatabaseSettings>();

        services.Configure<CatalogDatabaseSettings>(configuration.GetSection("CatalogDatabase"));

        services.AddDbContext<MotorcycleDbContext>(options =>
            options.UseMongoDB(mongoDBSettings.ConnectionString, mongoDBSettings.DatabaseName));

        return services;
    }
}
