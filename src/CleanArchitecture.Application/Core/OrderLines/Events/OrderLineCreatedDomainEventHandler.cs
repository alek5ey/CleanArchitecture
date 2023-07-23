using CleanArchitecture.Domain.DomainEvents.OrderLines;
using CleanArchitecture.Domain.Primitives.DomainEvents;
using CleanArchitecture.IntegrationEvents.OrderLines;
using MassTransit;

namespace CleanArchitecture.Application.Core.OrderLines.Events;

internal sealed class OrderLineCreatedDomainEventHandler : IDomainEventHandler<OrderLineCreatedDomainEvent>
{
    private readonly IBus _bus;

    public OrderLineCreatedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderLineCreatedDomainEvent notification, CancellationToken cancellationToken) =>
        await _bus.Publish(new OrderLineCreatedIntegraionEvent(notification.OrderLineId.Id), cancellationToken);
}