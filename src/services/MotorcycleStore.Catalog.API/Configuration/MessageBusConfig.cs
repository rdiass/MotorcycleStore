using MotorcycleStore.Catalog.API.Services;
using MotorcycleStore.Core.Utils;
using MotorcycleStore.MessaBus;

namespace MotorcycleStore.Catalog.API.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
            .AddHostedService<RegisterClientIntegrationHandler>();
    }
}
