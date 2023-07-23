using CleanArchitecture.Application.Core.Customers.Commands.DeleteCustomer;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.DomainEvents.Customers;
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Repositories.Customers;
using FluentResults;

namespace CleanArchitecture.UnitTests.Application.Core.Customers.Commands.DeleteCustomer;

public class DeleteCustomerCommandHandlerTests
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
        Customer? deletedEntity = null;
        var mockCustomerWriteRepository = new Mock<ICustomerWriteRepository>();
        mockCustomerWriteRepository.Setup(r => r.Remove(It.IsAny<Customer>()))
            .Callback((Customer entity) => deletedEntity = entity);
        var command = new DeleteCustomerCommand(customerId);
        var mockHandler = new DeleteCustomerCommandHandler(mockCustomerReadRepository.Object, mockCustomerWriteRepository.Object);
        
        var result = await mockHandler.Handle(command, CancellationToken.None);

        result.Should().BeOfType(typeof(Result));
        result.IsSuccess.Should().BeTrue();
        deletedEntity!.Should().Be(customer);
        customer.GetDomainEvents().Should().ContainItemsAssignableTo<CustomerDeletedDomainEvent>();
    }
    
    [Fact]
    public async Task Handle_WithCommand_ShouldNotFoundError()
    {
        var customerId = new CustomerId(Guid.NewGuid());
        var mockCustomerReadRepository = new Mock<ICustomerReadRepository>();
        mockCustomerReadRepository.Setup(r => r.GetByIdAsync(It.IsAny<CustomerId>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult((Customer?)null));
        var mockCustomerWriteRepository = new Mock<ICustomerWriteRepository>();
        var command = new DeleteCustomerCommand(customerId);
        var mockHandler = new DeleteCustomerCommandHandler(mockCustomerReadRepository.Object, mockCustomerWriteRepository.Object);
        
        var result = await mockHandler.Handle(command, CancellationToken.None);

        result.Should().BeOfType(typeof(Result));
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().Should().BeOfType<NotFoundError>();
        ((NotFoundError)result.Errors.First()).Code.Should().Be(CleanArchitecture.Application.Exceptions.ConstantErrors.Customers.NotFoundById.Code);
        ((NotFoundError)result.Errors.First()).Metadata.Should().Contain(m => m.Key == "Id" && (CustomerId)m.Value == customerId);
    }
}