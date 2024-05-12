using MotorcycleStore.Core.DomainObjects;
using MotorcycleStore.Core.Mediator;

namespace MotorcycleStore.Client.API.Extensions;

public static class MediatorExtension
{
    public static async Task PublishEvents<T>(this IMediatorHandler mediator, T entity) where T : Entity
    {
        var domainEvents = entity.DomainEvents.ToList();

        entity.ClearDomainEvents();

        var tasks = domainEvents.Select(async (domainEvent) =>
        {
            await mediator.RaiseEvent(domainEvent);
        });

        await Task.WhenAll(tasks);
    }
}