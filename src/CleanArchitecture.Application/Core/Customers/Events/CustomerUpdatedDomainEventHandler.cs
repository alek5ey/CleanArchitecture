using CleanArchitecture.Domain.DomainEvents.Customers;
using CleanArchitecture.Domain.Primitives.DomainEvents;
using CleanArchitecture.IntegrationEvents.Customers;
using MassTransit;

namespace CleanArchitecture.Application.Core.Customers.Events;

internal sealed class CustomerUpdatedDomainEventHandler : IDomainEventHandler<CustomerUpdatedDomainEvent>
{
    private readonly IBus _bus;

    public CustomerUpdatedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(CustomerUpdatedDomainEvent notification, CancellationToken cancellationToken) =>
        await _bus.Publish(new CustomerUpdatedIntegraionEvent(notification.CustomerId.Id), cancellationToken);
}