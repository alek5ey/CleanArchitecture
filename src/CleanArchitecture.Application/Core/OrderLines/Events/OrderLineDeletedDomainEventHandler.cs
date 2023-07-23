using CleanArchitecture.Domain.DomainEvents.OrderLines;
using CleanArchitecture.Domain.Primitives.DomainEvents;
using CleanArchitecture.IntegrationEvents.OrderLines;
using MassTransit;

namespace CleanArchitecture.Application.Core.OrderLines.Events;

internal sealed class OrderLineDeletedDomainEventHandler : IDomainEventHandler<OrderLineDeletedDomainEvent>
{
    private readonly IBus _bus;

    public OrderLineDeletedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderLineDeletedDomainEvent notification, CancellationToken cancellationToken) =>
        await _bus.Publish(new OrderLineDeletedIntegraionEvent(notification.OrderLineId.Id), cancellationToken);
}