using CleanArchitecture.Domain.Primitives.DomainEvents;

namespace CleanArchitecture.Domain.Primitives;

public interface IDomainEntity
{
    bool HasDomainEvents { get; }
    IReadOnlyCollection<DomainEvent> GetDomainEvents();
}