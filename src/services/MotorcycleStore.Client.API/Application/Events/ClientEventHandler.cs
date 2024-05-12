using MediatR;

namespace MotorcycleStore.Client.API.Application.Events;

public class ClientEventHandler : INotificationHandler<ClientRegisteredEvent>
{
    public Task Handle(ClientRegisteredEvent notification, CancellationToken cancellationToken)
    {
        // event handling
        return Task.CompletedTask;
    }
}
