using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Primitives.DomainEvents;

namespace CleanArchitecture.Domain.DomainEvents.Orders;

public record OrderUpdatedDomainEvent(Guid Id, OrderId OrderId) : DomainEvent(Id);