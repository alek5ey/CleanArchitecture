using CleanArchitecture.Domain.DomainEvents.Products;
using CleanArchitecture.Domain.Primitives.DomainEvents;
using CleanArchitecture.IntegrationEvents.Products;
using MassTransit;

namespace CleanArchitecture.Application.Core.Products.Events;

internal sealed class ProductUpdatedDomainEventHandler : IDomainEventHandler<ProductUpdatedDomainEvent>
{
    private readonly IBus _bus;

    public ProductUpdatedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(ProductUpdatedDomainEvent notification, CancellationToken cancellationToken) =>
        await _bus.Publish(new ProductUpdatedIntegraionEvent(notification.ProductId.Id), cancellationToken);
}