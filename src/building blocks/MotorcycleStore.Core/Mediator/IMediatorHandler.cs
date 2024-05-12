using FluentValidation.Results;
using MotorcycleStore.Core.Messages;

namespace MotorcycleStore.Core.Mediator;

public interface IMediatorHandler
{
    Task RaiseEvent<T>(T _event) where T : Event;
    Task<ValidationResult> SendCommand<T>(T command) where T : Command;
}
