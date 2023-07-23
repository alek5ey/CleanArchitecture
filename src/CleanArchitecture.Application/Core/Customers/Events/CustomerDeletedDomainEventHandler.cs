using CleanArchitecture.Domain.DomainEvents.Customers;
using CleanArchitecture.Domain.Primitives.DomainEvents;
using CleanArchitecture.IntegrationEvents.Customers;
using MassTransit;

namespace CleanArchitecture.Application.Core.Customers.Events;

internal sealed class CustomerDeletedDomainEventHandler : IDomainEventHandler<CustomerDeletedDomainEvent>
{
    private readonly IBus _bus;

    public CustomerDeletedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(CustomerDeletedDomainEvent notification, CancellationToken cancellationToken) =>
        await _bus.Publish(new CustomerDeletedIntegraionEvent(notification.CustomerId.Id), cancellationToken);
}