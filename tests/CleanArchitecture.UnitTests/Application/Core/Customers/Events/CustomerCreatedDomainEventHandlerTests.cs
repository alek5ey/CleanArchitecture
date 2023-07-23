using CleanArchitecture.Application.Core.Customers.Events;
using CleanArchitecture.Domain.DomainEvents.Customers;
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.IntegrationEvents.Customers;
using MassTransit;

namespace CleanArchitecture.UnitTests.Application.Core.Customers.Events;

public class CustomerCreatedDomainEventHandlerTests
{
    [Fact]
    public async Task Handle_WithDomainEvent_ShouldSuccessful()
    {
        var domainEventId = Guid.NewGuid();
        var customerId = new CustomerId(Guid.NewGuid());
        CustomerCreatedIntegraionEvent? publishedEvent = null;
        var mockIBus = new Mock<IBus>();
        mockIBus.Setup(r => r.Publish(It.IsAny<CustomerCreatedIntegraionEvent>(), It.IsAny<CancellationToken>()))
            .Callback((CustomerCreatedIntegraionEvent message, CancellationToken _) => publishedEvent = message);
        var @event = new CustomerCreatedDomainEvent(domainEventId, customerId);
        var mockHandler = new CustomerCreatedDomainEventHandler(mockIBus.Object);
        
        await mockHandler.Handle(@event, CancellationToken.None);
        
        publishedEvent!.CustomerId.Should().Be(customerId.Id);
    }
}