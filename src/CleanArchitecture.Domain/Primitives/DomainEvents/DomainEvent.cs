using MediatR;

namespace CleanArchitecture.Domain.Primitives.DomainEvents;

public abstract record DomainEvent(Guid Id) : INotification;