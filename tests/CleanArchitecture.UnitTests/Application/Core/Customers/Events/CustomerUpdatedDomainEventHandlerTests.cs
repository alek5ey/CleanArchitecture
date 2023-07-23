using CleanArchitecture.Application.Core.Customers.Events;
using CleanArchitecture.Domain.DomainEvents.Customers;
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.IntegrationEvents.Customers;
using MassTransit;

namespace CleanArchitecture.UnitTests.Application.Core.Customers.Events;

public class CustomerUpdatedDomainEventHandlerTests
{
    [Fact]
    public async Task Handle_WithDomainEvent_ShouldSuccessful()
    {
        var domainEventId = Guid.NewGuid();
        var customerId = new CustomerId(Guid.NewGuid());
        CustomerUpdatedIntegraionEvent? publishedEvent = null;
        var mockIBus = new Mock<IBus>();
        mockIBus.Setup(r => r.Publish(It.IsAny<CustomerUpdatedIntegraionEvent>(), It.IsAny<CancellationToken>()))
            .Callback((CustomerUpdatedIntegraionEvent message, CancellationToken _) => publishedEvent = message);
        var @event = new CustomerUpdatedDomainEvent(domainEventId, customerId);
        var mockHandler = new CustomerUpdatedDomainEventHandler(mockIBus.Object);
        
        await mockHandler.Handle(@event, CancellationToken.None);
        
        publishedEvent!.CustomerId.Should().Be(customerId.Id);
    }
}