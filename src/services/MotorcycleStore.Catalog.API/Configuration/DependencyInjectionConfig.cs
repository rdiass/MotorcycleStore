using MediatR;
using MotorcycleStore.Catalog.API.Data;
using MotorcycleStore.Catalog.API.Services;
using MotorcycleStore.Core.Mediator;
using MotorcycleStore.Catalog.API.Application.Commands;
using FluentValidation.Results;
using MotorcycleStore.Catalog.API.Application.Events;

namespace MotorcycleStore.Catalog.API.Configuration;

public static class DependencyInjectionConfig
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<IMediatorHandler, MediatorHandler>();
        services.AddScoped<IRequestHandler<RegisterClientCommand, ValidationResult>, ClientCommandHandler>();

        services.AddScoped<INotificationHandler<ClientRegisteredEvent>, ClientEventHandler>();

        services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
        services.AddScoped<MotorcycleService>();
    }
}
