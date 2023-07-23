using CleanArchitecture.Domain.DomainEvents.Customers;
using CleanArchitecture.Domain.Primitives.DomainEvents;
using CleanArchitecture.IntegrationEvents.Customers;
using MassTransit;

namespace CleanArchitecture.Application.Core.Customers.Events;

internal sealed class CustomerCreatedDomainEventHandler : IDomainEventHandler<CustomerCreatedDomainEvent>
{
    private readonly IBus _bus;

    public CustomerCreatedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(CustomerCreatedDomainEvent notification, CancellationToken cancellationToken) =>
        await _bus.Publish(new CustomerCreatedIntegraionEvent(notification.CustomerId.Id), cancellationToken);
}