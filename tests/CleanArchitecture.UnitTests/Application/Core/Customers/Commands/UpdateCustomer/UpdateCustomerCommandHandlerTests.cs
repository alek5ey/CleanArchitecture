using CleanArchitecture.Application.Core.Customers.Commands.UpdateCustomer;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.DomainEvents.Customers;
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Repositories.Customers;
using FluentResults;

namespace CleanArchitecture.UnitTests.Application.Core.Customers.Commands.UpdateCustomer;

public class UpdateCustomerCommandHandlerTests
{
    [Fact]
    public async Task Handle_WithCommand_ShouldSuccessful()
    {
        var customerId = new CustomerId(Guid.NewGuid());
        var customer = new Faker<Customer>()
            .CustomInstantiator(f => Customer.Create(customerId, f.Person.Email, f.Person.FullName, f.Address.FullAddress()))
            .Generate();
        var mockCustomerReadRepository = new Mock<ICustomerReadRepository>();
        mockCustomerReadRepository.Setup(r => r.GetByIdAsync(It.IsAny<CustomerId>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(customer)!);
        Customer? updatedEntity = null;
        var mockCustomerWriteRepository = new Mock<ICustomerWriteRepository>();
        mockCustomerWriteRepository.Setup(r => r.Update(It.IsAny<Customer>()))
            .Callback((Customer entity) => updatedEntity = entity);
        var command = new Faker<UpdateCustomerCommand>()
            .RuleFor(c => c.CustomerId, _ => customerId)
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Address, f => f.Address.FullAddress())
            .Generate();
        var mockHandler = new UpdateCustomerCommandHandler(mockCustomerReadRepository.Object, mockCustomerWriteRepository.Object);
        
        var result = await mockHandler.Handle(command, CancellationToken.None);
        
        result.Should().BeOfType(typeof(Result));
        result.IsSuccess.Should().BeTrue();
        updatedEntity.Should().NotBeNull();
        updatedEntity!.Id.Should().Be(customerId).And.Be(customer.Id);
        updatedEntity.Email.Should().Be(command.Email).And.Be(customer.Email);
        updatedEntity.Name.Should().Be(command.Name).And.Be(customer.Name);
        updatedEntity.Address.Should().Be(command.Address).And.Be(customer.Address);
        customer.GetDomainEvents().Should().ContainItemsAssignableTo<CustomerUpdatedDomainEvent>();
    }
    
    [Fact]
    public async Task Handle_WithCommand_ShouldNotFoundError()
    {
        var customerId = new CustomerId(Guid.NewGuid());
        var mockCustomerReadRepository = new Mock<ICustomerReadRepository>();
        mockCustomerReadRepository.Setup(r => r.GetByIdAsync(It.IsAny<CustomerId>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult((Customer?)null));
        var mockCustomerWriteRepository = new Mock<ICustomerWriteRepository>();
        var command = new Faker<UpdateCustomerCommand>()
            .RuleFor(c => c.CustomerId, _ => customerId)
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Address, f => f.Address.FullAddress())
            .Generate();
        var mockHandler = new UpdateCustomerCommandHandler(mockCustomerReadRepository.Object, mockCustomerWriteRepository.Object);
        
        var result = await mockHandler.Handle(command, CancellationToken.None);
        
        result.Should().BeOfType(typeof(Result));
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().Should().BeOfType<NotFoundError>();
        ((NotFoundError)result.Errors.First()).Code.Should().Be(CleanArchitecture.Application.Exceptions.ConstantErrors.Customers.NotFoundById.Code);
        ((NotFoundError)result.Errors.First()).Metadata.Should().Contain(m => m.Key == "Id" && (CustomerId)m.Value == customerId);
    }
}