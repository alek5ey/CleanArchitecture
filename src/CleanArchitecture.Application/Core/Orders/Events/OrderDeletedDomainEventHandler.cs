using CleanArchitecture.Domain.DomainEvents.Orders;
using CleanArchitecture.Domain.Primitives.DomainEvents;
using CleanArchitecture.IntegrationEvents.Orders;
using MassTransit;

namespace CleanArchitecture.Application.Core.Orders.Events;

internal sealed class OrderDeletedDomainEventHandler : IDomainEventHandler<OrderDeletedDomainEvent>
{
    private readonly IBus _bus;

    public OrderDeletedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderDeletedDomainEvent notification, CancellationToken cancellationToken) =>
        await _bus.Publish(new OrderDeletedIntegraionEvent(notification.OrderId.Id), cancellationToken);
}