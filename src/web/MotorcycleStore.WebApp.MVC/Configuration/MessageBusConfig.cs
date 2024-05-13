using MotorcycleStore.Core.Utils;
using MotorcycleStore.MessaBus;

namespace MotorcycleStore.WebApp.MVC.Configuration;

public static class MessageBusConfig
{
    public static void AddMessageBusConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
    }
}
