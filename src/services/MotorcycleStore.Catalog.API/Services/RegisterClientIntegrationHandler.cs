using MotorcycleStore.Core.Messages.Integration;
using MotorcycleStore.Catalog.API.Application.Commands;
using MotorcycleStore.Core.Mediator;
using FluentValidation.Results;
using MotorcycleStore.MessaBus;

namespace MotorcycleStore.Catalog.API.Services;

public class RegisterClientIntegrationHandler : BackgroundService
{
    private readonly IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public RegisterClientIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
    {
        _serviceProvider = serviceProvider;
        _bus = bus;
    }

    private void SetResponder()
    {
        _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(RegisterClient);
        _bus.AdvancedBus.Connected += OnConnect;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        SetResponder();
        return Task.CompletedTask;
    }

    private void OnConnect(object s, EventArgs e)
    {
        _bus.RespondAsync<UserRegisteredIntegrationEvent, ResponseMessage>(RegisterClient);
    }

    public async Task<ResponseMessage> RegisterClient(UserRegisteredIntegrationEvent request)
    {
        var clientCommand = new RegisterClientCommand(
            request.Id,
            request.Name,
            request.Cnpj,
            request.BirthDate,
            request.Cnh,
            request.TypeCnh,
            request.CnhImage);

        ValidationResult success;

        using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
            success = await mediator.SendCommand(clientCommand);
        }

        return new ResponseMessage(success);
    }
}
