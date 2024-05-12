using Microsoft.EntityFrameworkCore;
using MotorcycleStore.Client.API.Data;
using MotorcycleStore.Client.API.Settings;

namespace MotorcycleStore.Client.API.Configuration;

public static class MongoDbConfiguration
{
    public static IServiceCollection AddMongoDbConfig(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDBSettings = configuration.GetSection("DeliveryManDatabase").Get<DeliveryManDatabaseSettings>();

        services.Configure<DeliveryManDatabaseSettings>(configuration.GetSection("DeliveryManDatabase"));

        services.AddDbContext<DeliveryManDbContext>(options =>
            options.UseMongoDB(mongoDBSettings.ConnectionString, mongoDBSettings.DatabaseName));

        return services;
    }
}
