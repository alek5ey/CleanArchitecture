using CleanArchitecture.Domain.DomainEvents.Customers;
using CleanArchitecture.Domain.Entites.Customers;

namespace CleanArchitecture.UnitTests.Domain.Entities.Customers;

public class CustomerTests
{
    [Fact]
    public void Create_WithParams_Successful()
    {
        var faker = new Faker();
        var customerId = new CustomerId(Guid.NewGuid());
        var email = faker.Person.Email;
        var name = faker.Person.FullName;
        var address = faker.Address.FullAddress();

        var customer = CreateCustomerSample(
            customerId,
            email,
            name,
            address);

        customer.Id.Should().Be(customerId);
        customer.Email.Should().Be(email);
        customer.Name.Should().Be(name);
        customer.Address.Should().Be(address);
        customer.HasDomainEvents.Should().BeTrue();
        customer.GetDomainEvents().Should().HaveCount(1);
        customer.GetDomainEvents().First().Should().BeOfType<CustomerCreatedDomainEvent>();
        ((CustomerCreatedDomainEvent)customer.GetDomainEvents().First()).CustomerId.Should().Be(customerId);
    }
    
    [Fact]
    public void Update_WithParams_Successful()
    {
        var customerId = new CustomerId(Guid.NewGuid());
        var customer = CreateCustomerSample(customerId);
        var updatedCustomer = CreateCustomerSample();
        
        customer.Update(
            updatedCustomer.Email,
            updatedCustomer.Name,
            updatedCustomer.Address);
        
        customer.Id.Should().Be(customerId);
        customer.Email.Should().Be(updatedCustomer.Email);
        customer.Name.Should().Be(updatedCustomer.Name);
        customer.Address.Should().Be(updatedCustomer.Address);
        customer.HasDomainEvents.Should().BeTrue();
        customer.GetDomainEvents().Should().HaveCount(2);
        customer.GetDomainEvents().Should().ContainItemsAssignableTo<CustomerUpdatedDomainEvent>();
        ((CustomerUpdatedDomainEvent)customer.GetDomainEvents().Last()).CustomerId.Should().Be(customerId);
    }
    
    [Fact]
    public void Delete_Successful()
    {
        var customer = CreateCustomerSample();
        
        customer.Delete();
        
        customer.HasDomainEvents.Should().BeTrue();
        customer.GetDomainEvents().Should().HaveCount(2);
        customer.GetDomainEvents().Should().ContainItemsAssignableTo<CustomerDeletedDomainEvent>();
        ((CustomerDeletedDomainEvent)customer.GetDomainEvents().Last()).CustomerId.Should().Be(customer.Id);
    }
    
    private static Customer CreateCustomerSample(
        CustomerId? customerId = null,
        string? email = null,
        string? name = null,
        string? address = null)
    {
        var faker = new Faker();
        customerId ??= new CustomerId(Guid.NewGuid());
        email ??= faker.Person.Email;
        name ??= faker.Person.FullName;
        address ??= faker.Address.FullAddress();

        return new Faker<Customer>()
            .CustomInstantiator(_ => Customer.Create(customerId, email, name, address))
            .Generate();
    }
}