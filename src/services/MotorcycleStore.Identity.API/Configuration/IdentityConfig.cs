using Microsoft.AspNetCore.Identity;
using MotorcycleStore.WebAPI.Core.Identity;
using MS.Identity.API.Models;
using MS.Identity.API.Settings;

namespace MotorcycleStore.Identity.API.Configuration;

public static class IdentityConfig
{
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        var appSettingSection = configuration.GetSection(nameof(AppSettings));
        var mongoDbSettings = configuration.GetSection(nameof(MongoDbConfig)).Get<MongoDbConfig>();

        services.Configure<AppSettings>(appSettingSection);

        services
        .AddAuthorization()
        .AddIdentity<ApplicationUser, ApplicationRole>()
        .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
        (
            mongoDbSettings.ConnectionString, mongoDbSettings.Name
        )
        .AddDefaultTokenProviders();

        // JWT
        services.AddJwtConfiguration(configuration);

        return services;
    }
}
