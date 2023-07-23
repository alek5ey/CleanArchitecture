using CleanArchitecture.Domain.DomainEvents.Orders;
using CleanArchitecture.Domain.Primitives.DomainEvents;
using CleanArchitecture.IntegrationEvents.Orders;
using MassTransit;

namespace CleanArchitecture.Application.Core.Orders.Events;

internal sealed class OrderUpdatedDomainEventHandler : IDomainEventHandler<OrderUpdatedDomainEvent>
{
    private readonly IBus _bus;

    public OrderUpdatedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(OrderUpdatedDomainEvent notification, CancellationToken cancellationToken) =>
        await _bus.Publish(new OrderUpdatedIntegraionEvent(notification.OrderId.Id), cancellationToken);
}