using FluentValidation.Results;
using MediatR;
using MotorcycleStore.Client.API.Application.Commands;
using MotorcycleStore.Client.API.Application.Events;
using MotorcycleStore.Client.API.Data;
using MotorcycleStore.Client.API.Services;
using MotorcycleStore.Core.Mediator;

namespace MotorcycleStore.Client.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, ClientCommandHandler>();
        services.AddScoped<INotificationHandler<ClientRegisteredEvent>, ClientEventHandler>();
        services.AddScoped<IDeliveryManRepository, DeliveryManRepository>();
        services.AddScoped<DeliveryManService>();
    }
}
