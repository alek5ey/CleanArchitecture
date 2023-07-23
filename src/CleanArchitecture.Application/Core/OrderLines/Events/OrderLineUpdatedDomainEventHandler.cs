using CleanArchitecture.Domain.DomainEvents.OrderLines;
using CleanArchitecture.Domain.Primitives.DomainEvents;
using CleanArchitecture.IntegrationEvents.OrderLines;
using MassTransit;

namespace CleanArchitecture.Application.Core.OrderLines.Events;

internal sealed class OrderLineUpdatedDomainEventHandler : IDomainEventHandler<OrderLineUpdatedDomainEvent>
{
    private readonly IBus _bus;

    public OrderLineUpdatedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderLineUpdatedDomainEvent notification, CancellationToken cancellationToken) =>
        await _bus.Publish(new OrderLineUpdatedIntegraionEvent(notification.OrderLineId.Id), cancellationToken);
}