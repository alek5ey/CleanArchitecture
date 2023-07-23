using CleanArchitecture.Domain.DomainEvents.OrderLines;
using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Entites.Products;

namespace CleanArchitecture.UnitTests.Domain.Entities.OrderLines;

public class OrderLineTests
{
    [Fact]
    public void Create_WithParams_Successful()
    {
        var faker = new Faker();
        var orderLineId = new OrderLineId(Guid.NewGuid());
        var orderId = new OrderId(Guid.NewGuid());
        var productId = new ProductId(Guid.NewGuid());;
        var money = Money.Create(faker.Finance.Currency().Code!, faker.Random.Decimal(0m, 100m));
        var quantity = faker.Random.Int(1, 100);

        var orderLine = CreateOrderLineSample(
            orderLineId,
            orderId,
            productId,
            money,
            quantity);

        orderLine.Id.Should().Be(orderLineId);
        orderLine.OrderId.Should().Be(orderId);
        orderLine.ProductId.Should().Be(productId);
        orderLine.Price.Should().Be(money);
        orderLine.Quantity.Should().Be(quantity);
        orderLine.HasDomainEvents.Should().BeTrue();
        orderLine.GetDomainEvents().Should().HaveCount(1);
        orderLine.GetDomainEvents().First().Should().BeOfType<OrderLineCreatedDomainEvent>();
        ((OrderLineCreatedDomainEvent)orderLine.GetDomainEvents().First()).OrderLineId.Should().Be(orderLineId);
    }
    
    [Fact]
    public void Update_WithParams_Successful()
    {
        var orderLineId = new OrderLineId(Guid.NewGuid());
        var orderLine = CreateOrderLineSample(orderLineId);
        var updatedOrderLine = CreateOrderLineSample();
        
        orderLine.Update(
            updatedOrderLine.OrderId,
            updatedOrderLine.ProductId,
            updatedOrderLine.Price,
            updatedOrderLine.Quantity);
        
        orderLine.Id.Should().Be(orderLineId);
        orderLine.OrderId.Should().Be(updatedOrderLine.OrderId);
        orderLine.ProductId.Should().Be(updatedOrderLine.ProductId);
        orderLine.Price.Should().Be(updatedOrderLine.Price);
        orderLine.Quantity.Should().Be(updatedOrderLine.Quantity);
        orderLine.HasDomainEvents.Should().BeTrue();
        orderLine.GetDomainEvents().Should().HaveCount(2);
        orderLine.GetDomainEvents().Should().ContainItemsAssignableTo<OrderLineUpdatedDomainEvent>();
        ((OrderLineUpdatedDomainEvent)orderLine.GetDomainEvents().Last()).OrderLineId.Should().Be(orderLineId);
    }
    
    [Fact]
    public void Delete_Successful()
    {
        var orderLine = CreateOrderLineSample();
        
        orderLine.Delete();
        
        orderLine.HasDomainEvents.Should().BeTrue();
        orderLine.GetDomainEvents().Should().HaveCount(2);
        orderLine.GetDomainEvents().Should().ContainItemsAssignableTo<OrderLineDeletedDomainEvent>();
        ((OrderLineDeletedDomainEvent)orderLine.GetDomainEvents().Last()).OrderLineId.Should().Be(orderLine.Id);
    }
    
    private static OrderLine CreateOrderLineSample(
        OrderLineId? orderLineId = null,
        OrderId? orderId = null,
        ProductId? productId = null,
        Money? money = null,
        int? quantity = null)
    {
        var faker = new Faker();
        orderLineId ??= new OrderLineId(Guid.NewGuid());
        orderId ??= new OrderId(Guid.NewGuid());
        productId ??= new ProductId(Guid.NewGuid());
        money ??= Money.Create(faker.Finance.Currency().Code!, faker.Random.Decimal(0m, 100m));
        quantity ??= faker.Random.Int(1, 100);

        return new Faker<OrderLine>()
            .CustomInstantiator(_ => OrderLine.Create(orderLineId, orderId, productId, money, quantity.Value))
            .Generate();
    }
}