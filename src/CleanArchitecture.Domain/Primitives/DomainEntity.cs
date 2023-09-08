using CleanArchitecture.Domain.Primitives.DomainEvents;

namespace CleanArchitecture.Domain.Primitives;

public abstract class DomainEntity<TEntityId> : IDomainEntity
{
    private readonly Queue<DomainEvent> _domainEvents = new();

    public TEntityId Id { get; protected init; }

    public bool HasDomainEvents => _domainEvents.Any();

    protected void Raise(DomainEvent domainEvent) => _domainEvents.Enqueue(domainEvent);
    
    public IReadOnlyCollection<DomainEvent> GetDomainEvents() => _domainEvents;
}