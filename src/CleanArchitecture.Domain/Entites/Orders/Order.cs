using CleanArchitecture.Domain.DomainEvents.Orders;
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Primitives;

namespace CleanArchitecture.Domain.Entites.Orders;

public class Order : DomainEntity<OrderId>
{
    public CustomerId CustomerId { get; private set; }
    public DateTimeOffset CreateTime { get; private set; }
    public string? Comment { get; private set; }

    public readonly IReadOnlyList<OrderLine> OrderLines = new List<OrderLine>();
    public decimal TotalPrice => OrderLines.Sum(o => o.TotalPrice);

    #pragma warning disable CS8618 // Required by Entity Framework
    private Order() { }
    
    public static Order Create(OrderId id, CustomerId customerId, DateTimeOffset createTime, string? comment)
    {
        var order = new Order
        {
            Id = id,
            CustomerId = customerId,
            CreateTime = createTime,
            Comment = comment
        };

        order.Raise(new OrderCreatedDomainEvent(Guid.NewGuid(), order.Id));
        
        return order;
    }
    
    public void Update(CustomerId customerId, string? comment)
    {
        CustomerId = customerId;
        Comment = comment;
        
        Raise(new OrderUpdatedDomainEvent(Guid.NewGuid(), Id));
    }

    public void Delete()
    {
        foreach (var orderLine in OrderLines)
            orderLine.Delete();
        
        Raise(new OrderDeletedDomainEvent(Guid.NewGuid(), Id));
    }
}