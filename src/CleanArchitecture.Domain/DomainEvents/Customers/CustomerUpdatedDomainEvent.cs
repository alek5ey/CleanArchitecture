using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Primitives.DomainEvents;

namespace CleanArchitecture.Domain.DomainEvents.Customers;

public record CustomerUpdatedDomainEvent(Guid Id, CustomerId CustomerId) : DomainEvent(Id);