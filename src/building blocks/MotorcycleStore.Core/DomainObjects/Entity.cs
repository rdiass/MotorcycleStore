using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MotorcycleStore.Core.Messages;

namespace MotorcycleStore.Core.DomainObjects;

public abstract class Entity
{
    [BsonId]
    public ObjectId? Id { get; set; }

    private List<Event> _domainEvents;
    public IReadOnlyCollection<Event> DomainEvents => _domainEvents?.AsReadOnly();

    public void AddDomainEvent(Event eventItem)
    {
        _domainEvents = _domainEvents ?? new List<Event>();
        _domainEvents.Add(eventItem);
    }

    public void RemoveDomainEvent(Event eventItem)
    {
        _domainEvents?.Remove(eventItem);
    }

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    public override bool Equals(object obj)
    {
        var compareTo = obj as Entity;

        if (ReferenceEquals(this, compareTo)) return true;
        if (ReferenceEquals(null, compareTo)) return false;

        return Id.Equals(compareTo.Id);
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }

    public static bool operator ==(Entity a, Entity b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null)) return true;
        if (ReferenceEquals(a, null) || ReferenceEquals(b, null)) return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity a, Entity b)
    {
        return !(a == b);
    }
}