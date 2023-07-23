using CleanArchitecture.Domain.DomainEvents.Orders;
using CleanArchitecture.Domain.Primitives.DomainEvents;
using CleanArchitecture.IntegrationEvents.Orders;
using MassTransit;

namespace CleanArchitecture.Application.Core.Orders.Events;

internal sealed class OrderCreatedDomainEventHandler : IDomainEventHandler<OrderCreatedDomainEvent>
{
    private readonly IBus _bus;

    public OrderCreatedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderCreatedDomainEvent notification, CancellationToken cancellationToken) =>
        await _bus.Publish(new OrderCreatedIntegraionEvent(notification.OrderId.Id), cancellationToken);
}