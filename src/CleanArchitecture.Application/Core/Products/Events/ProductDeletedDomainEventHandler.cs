using CleanArchitecture.Domain.DomainEvents.Products;
using CleanArchitecture.Domain.Primitives.DomainEvents;
using CleanArchitecture.IntegrationEvents.Products;
using MassTransit;

namespace CleanArchitecture.Application.Core.Products.Events;

internal sealed class ProductDeletedDomainEventHandler : IDomainEventHandler<ProductDeletedDomainEvent>
{
    private readonly IBus _bus;

    public ProductDeletedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(ProductDeletedDomainEvent notification, CancellationToken cancellationToken) =>
        await _bus.Publish(new ProductDeletedIntegraionEvent(notification.ProductId.Id), cancellationToken);
}