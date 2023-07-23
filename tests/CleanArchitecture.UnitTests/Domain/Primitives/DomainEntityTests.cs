using CleanArchitecture.Domain.Primitives;
using CleanArchitecture.Domain.Primitives.DomainEvents;

namespace CleanArchitecture.UnitTests.Domain.Primitives;

public class DomainEntityTests
{
    [Fact]
    public void Raise_WithDomainEvent_Successful()
    {
        var eventId = Guid.NewGuid();
        var mockDomainEvent = new Mock<DomainEvent>(eventId);
        var domainEntity = new FakeDomainEntity();
        
        domainEntity.Raise(mockDomainEvent.Object);

        domainEntity.HasDomainEvents.Should().BeTrue();
        domainEntity.GetDomainEvents().Should().HaveCount(1);
    }

    private class FakeDomainEntity : DomainEntity
    {
        public new void Raise(DomainEvent domainEvent) => base.Raise(domainEvent);
    }
}