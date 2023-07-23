using CleanArchitecture.Application.Core.Customers.Commands.CreateCustomer;
using CleanArchitecture.Domain.DomainEvents.Customers;
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Repositories.Customers;
using FluentResults;

namespace CleanArchitecture.UnitTests.Application.Core.Customers.Commands.CreateCustomer;

public class CreateCustomerCommandHandlerTests
{
    [Fact]
    public async Task Handle_WithCommand_ShouldSuccessful()
    {
        Customer? addedEntity = null;
        var mockCustomerWriteRepository = new Mock<ICustomerWriteRepository>();
        mockCustomerWriteRepository.Setup(r => r.AddAsync(It.IsAny<Customer>(), It.IsAny<CancellationToken>()))
            .Callback((Customer entity, CancellationToken _) => addedEntity = entity);
        var command = new Faker<CreateCustomerCommand>()
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Address, f => f.Address.FullAddress())
            .Generate();
        var mockHandler = new CreateCustomerCommandHandler(mockCustomerWriteRepository.Object);
        
        var result = await mockHandler.Handle(command, CancellationToken.None);
        
        result.Should().BeOfType(typeof(Result));
        result.IsSuccess.Should().BeTrue();
        addedEntity.Should().NotBeNull();
        addedEntity!.Id.Id.Should().NotBe(Guid.Empty);
        addedEntity.Email.Should().Be(command.Email);
        addedEntity.Name.Should().Be(command.Name);
        addedEntity.Address.Should().Be(command.Address);
        addedEntity.GetDomainEvents().Should().ContainItemsAssignableTo<CustomerCreatedDomainEvent>();
    }
}