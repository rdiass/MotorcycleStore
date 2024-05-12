using FluentValidation.Results;
using MediatR;
using MotorcycleStore.Core.Messages;

namespace MotorcycleStore.Core.Mediator;

public class MediatorHandler : IMediatorHandler
{
    private readonly IMediator _mediator;

    public MediatorHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task RaiseEvent<T>(T @event) where T : Event
    {
        await _mediator.Publish(@event);
    }

    public async Task<ValidationResult> SendCommand<T>(T command) where T : Command
    {
        return await _mediator.Send(command);
    }
}
