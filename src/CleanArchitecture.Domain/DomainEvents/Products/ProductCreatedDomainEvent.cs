using CleanArchitecture.Domain.Entites.Products;
using CleanArchitecture.Domain.Primitives.DomainEvents;

namespace CleanArchitecture.Domain.DomainEvents.Products;

public record ProductCreatedDomainEvent(Guid Id, ProductId ProductId) : DomainEvent(Id);