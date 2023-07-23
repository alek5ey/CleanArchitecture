using CleanArchitecture.Domain.DomainEvents.Products;
using CleanArchitecture.Domain.Primitives.DomainEvents;
using CleanArchitecture.IntegrationEvents.Products;
using MassTransit;

namespace CleanArchitecture.Application.Core.Products.Events;

internal sealed class ProductCreatedDomainEventHandler : IDomainEventHandler<ProductCreatedDomainEvent>
{
    private readonly IBus _bus;

    public ProductCreatedDomainEventHandler(IBus bus)
    {
        _bus = bus;
    }

    public async Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken) =>
        await _bus.Publish(new ProductCreatedIntegraionEvent(notification.ProductId.Id), cancellationToken);
}