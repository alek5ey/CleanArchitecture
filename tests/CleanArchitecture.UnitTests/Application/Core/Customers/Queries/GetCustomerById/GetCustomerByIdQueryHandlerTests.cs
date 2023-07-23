using CleanArchitecture.Application.Contracts.Customers.Responses;
using CleanArchitecture.Application.Core.Customers.Queries.GetCustomerById;
using CleanArchitecture.Application.Exceptions;
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Repositories.Customers;
using FluentResults;

namespace CleanArchitecture.UnitTests.Application.Core.Customers.Queries.GetCustomerById;

public class GetCustomerByIdQueryHandlerTests
{
    [Fact]
    public async Task Handle_WithQuery_ShouldSuccessful()
    {
        var customerId = new CustomerId(Guid.NewGuid());
        var customer = new Faker<Customer>()
            .CustomInstantiator(f => Customer.Create(customerId, f.Person.Email, f.Person.FullName, f.Address.FullAddress()))
            .Generate();
        var mockCustomerReadRepository = new Mock<ICustomerReadRepository>();
        mockCustomerReadRepository.Setup(r => r.GetByIdAsync(It.IsAny<CustomerId>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult(customer)!);
        var query = new GetCustomerByIdQuery(customerId);
        var mockHandler = new GetCustomerByIdQueryHandler(mockCustomerReadRepository.Object);

        var result = await mockHandler.Handle(query, CancellationToken.None);

        result.Should().BeOfType(typeof(Result<CustomerResponse>));
        result.IsSuccess.Should().BeTrue();
        result.Value.Id.Should().Be(customerId.Id);
        result.Value.Email.Should().Be(customer.Email);
        result.Value.Name.Should().Be(customer.Name);
        result.Value.Address.Should().Be(customer.Address);
    }
    
    [Fact]
    public async Task Handle_WithQuery_ShouldNotFoundError()
    {
        var customerId = new CustomerId(Guid.NewGuid());
        var mockCustomerReadRepository = new Mock<ICustomerReadRepository>();
        mockCustomerReadRepository.Setup(r => r.GetByIdAsync(It.IsAny<CustomerId>(), It.IsAny<CancellationToken>()))
            .Returns(Task.FromResult((Customer?)null));
        var query = new GetCustomerByIdQuery(customerId);
        var mockHandler = new GetCustomerByIdQueryHandler(mockCustomerReadRepository.Object);
        
        var result = await mockHandler.Handle(query, CancellationToken.None);

        result.Should().BeOfType(typeof(Result<CustomerResponse>));
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().HaveCount(1);
        result.Errors.First().Should().BeOfType<NotFoundError>();
        ((NotFoundError)result.Errors.First()).Code.Should().Be(CleanArchitecture.Application.Exceptions.ConstantErrors.Customers.NotFoundById.Code);
        ((NotFoundError)result.Errors.First()).Metadata.Should().Contain(m => m.Key == "Id" && (CustomerId)m.Value == customerId);
    }
}