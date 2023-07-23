using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Primitives.DomainEvents;

namespace CleanArchitecture.Domain.DomainEvents.Orders;

public record OrderCreatedDomainEvent(Guid Id, OrderId OrderId) : DomainEvent(Id);