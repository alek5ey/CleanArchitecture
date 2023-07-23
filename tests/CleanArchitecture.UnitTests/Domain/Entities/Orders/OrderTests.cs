using CleanArchitecture.Domain.DomainEvents.Orders;
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Entites.Orders;

namespace CleanArchitecture.UnitTests.Domain.Entities.Orders;

public class OrderTests
{
    [Fact]
    public void Create_WithParams_Successful()
    {
        var faker = new Faker();
        var orderId = new OrderId(Guid.NewGuid());
        var customerId = new CustomerId(Guid.NewGuid());
        var createTime = faker.Date.PastOffset();
        var comment = faker.Lorem.Sentences();

        var order = CreateOrderSample(
            orderId,
            customerId,
            createTime,
            comment);

        order.Id.Should().Be(orderId);
        order.CustomerId.Should().Be(customerId);
        order.CreateTime.Should().Be(createTime);
        order.Comment.Should().Be(comment);
        order.HasDomainEvents.Should().BeTrue();
        order.GetDomainEvents().Should().HaveCount(1);
        order.GetDomainEvents().First().Should().BeOfType<OrderCreatedDomainEvent>();
        ((OrderCreatedDomainEvent)order.GetDomainEvents().First()).OrderId.Should().Be(orderId);
    }
    
    [Fact]
    public void Update_WithParams_Successful()
    {
        var orderId = new OrderId(Guid.NewGuid());
        var createTime = new Faker().Date.PastOffset();
        var order = CreateOrderSample(orderId, createTime: createTime);
        var updatedOrder = CreateOrderSample();
        
        order.Update(
            updatedOrder.CustomerId,
            updatedOrder.Comment);
        
        order.Id.Should().Be(orderId);
        order.CustomerId.Should().Be(updatedOrder.CustomerId);
        order.CreateTime.Should().Be(createTime);
        order.Comment.Should().Be(updatedOrder.Comment);
        order.HasDomainEvents.Should().BeTrue();
        order.GetDomainEvents().Should().HaveCount(2);
        order.GetDomainEvents().Should().ContainItemsAssignableTo<OrderUpdatedDomainEvent>();
        ((OrderUpdatedDomainEvent)order.GetDomainEvents().Last()).OrderId.Should().Be(orderId);
    }
    
    [Fact]
    public void Delete_Successful()
    {
        var order = CreateOrderSample();
        
        order.Delete();
        
        order.HasDomainEvents.Should().BeTrue();
        order.GetDomainEvents().Should().HaveCount(2);
        order.GetDomainEvents().Should().ContainItemsAssignableTo<OrderDeletedDomainEvent>();
        ((OrderDeletedDomainEvent)order.GetDomainEvents().Last()).OrderId.Should().Be(order.Id);
    }
    
    private static Order CreateOrderSample(
        OrderId? orderId = null,
        CustomerId? customerId = null,
        DateTimeOffset? createTime = null,
        string? comment = null)
    {
        var faker = new Faker();
        orderId ??= new OrderId(Guid.NewGuid());
        customerId ??= new CustomerId(Guid.NewGuid());
        createTime ??= faker.Date.PastOffset();
        comment ??= faker.Lorem.Sentences();

        return new Faker<Order>()
            .CustomInstantiator(_ => Order.Create(orderId, customerId, createTime.Value, comment))
            .Generate();
    }
}