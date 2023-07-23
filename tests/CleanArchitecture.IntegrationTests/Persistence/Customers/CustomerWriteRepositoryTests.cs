using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Persistence.Repositories.Customers;

namespace CleanArchitecture.IntegrationTests.Persistence.Customers;

public class CustomerWriteRepositoryTests : IClassFixture<DbTestFixture>
{
    private readonly DbTestFixture _fixture;

    public CustomerWriteRepositoryTests(DbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task AddAsync_ShouldSuccessful()
    {
        var customerId = new CustomerId(Guid.NewGuid());
        var customer = new Faker<Customer>()
            .CustomInstantiator(f => Customer.Create(customerId, f.Person.Email, f.Person.FullName, f.Address.FullAddress()))
            .Generate();
        var repository = new CustomerWriteRepository(_fixture.Context!);

        await repository.AddAsync(customer);
        await _fixture.SaveChangesAsync();
        var result = await _fixture.Context!.Customers.FirstOrDefaultAsync(c => c.Id == customerId);

        result.Should().NotBeNull();
        result!.Id.Should().Be(customerId);
        result.Email.Should().Be(customer.Email);
        result.Name.Should().Be(customer.Name);
        result.Address.Should().Be(customer.Address);
    }
    
    [Fact]
    public async Task Update_ShouldSuccessful()
    {
        var customerId = new CustomerId(Guid.Parse("47e43833-b417-418d-8779-14b1523a6903"));
        var customer = await _fixture.Context!.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        if (customer is null)
            throw new Exception("Not found test Customer by Id");
        var faker = new Faker();
        var email = faker.Person.Email;
        var name = faker.Person.FullName;
        var address = faker.Address.FullAddress();
        var repository = new CustomerWriteRepository(_fixture.Context);

        customer.Update(email, name, address);
        repository.Update(customer);
        await _fixture.SaveChangesAsync();
        var result = await _fixture.Context.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);

        result.Should().NotBeNull();
        result!.Id.Should().Be(customer.Id);
        result.Email.Should().Be(email);
        result.Name.Should().Be(name);
        result.Address.Should().Be(address);
    }
    
    [Fact]
    public async Task Remove_ShouldSuccessful()
    {
        var customerId = new CustomerId(Guid.Parse("bcb463ad-bfdf-4877-b4a7-683fc36291b6"));
        var customer = await _fixture.Context!.Customers.FirstOrDefaultAsync(c => c.Id == customerId);
        if (customer is null)
            throw new Exception("Not found test Customer by Id");
        var repository = new CustomerWriteRepository(_fixture.Context);
        
        repository.Remove(customer);
        await _fixture.SaveChangesAsync();
        var result = await _fixture.Context.Customers.FirstOrDefaultAsync(c => c.Id == customer.Id);

        result.Should().BeNull();
    }
}