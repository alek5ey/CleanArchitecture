using CleanArchitecture.Domain.Entites.Products;
using CleanArchitecture.Domain.Primitives.DomainEvents;

namespace CleanArchitecture.Domain.DomainEvents.Products;

public record ProductUpdatedDomainEvent(Guid Id, ProductId ProductId) : DomainEvent(Id);