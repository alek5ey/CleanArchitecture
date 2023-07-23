using CleanArchitecture.Domain.DomainEvents.OrderLines;
using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Entites.Products;
using CleanArchitecture.Domain.Primitives;

namespace CleanArchitecture.Domain.Entites.OrderLines;

public class OrderLine : DomainEntity
{
    public OrderLineId Id { get; private init; }
    public OrderId OrderId { get; private set; }
    public ProductId ProductId { get; private set; }
    public Money Price { get; private set; }
    public int Quantity { get; private set; }
    public decimal TotalPrice => Price.Amount * Quantity;

    #pragma warning disable CS8618 // Required by Entity Framework
    private OrderLine() { }
    
    public static OrderLine Create(OrderLineId id, OrderId orderId, ProductId productId, Money price, int quantity)
    {
        var orderLine = new OrderLine
        {
            Id = id,
            OrderId = orderId,
            ProductId = productId,
            Price = price,
            Quantity = quantity
        };
        
        orderLine.Raise(new OrderLineCreatedDomainEvent(Guid.NewGuid(), orderLine.Id));

        return orderLine;
    }

    public void Update(OrderId orderId, ProductId productId, Money price, int quantity)
    {
        OrderId = orderId;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
        
        Raise(new OrderLineUpdatedDomainEvent(Guid.NewGuid(), Id));
    }
    
    public void Delete() =>
        Raise(new OrderLineDeletedDomainEvent(Guid.NewGuid(), Id));
}