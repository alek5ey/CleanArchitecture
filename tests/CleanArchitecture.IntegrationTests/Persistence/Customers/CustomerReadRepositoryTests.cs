using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Persistence.Repositories.Customers;

namespace CleanArchitecture.IntegrationTests.Persistence.Customers;

public class CustomerReadRepositoryTests : IClassFixture<DbTestFixture>
{
    private readonly DbTestFixture _fixture;

    public CustomerReadRepositoryTests(DbTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task GetById_ByCustomerId_ShouldSuccessful()
    {
        var customerId = new CustomerId(Guid.Parse("47e43833-b417-418d-8779-14b1523a6903"));
        var repository = new CustomerReadRepository(_fixture.Context!);

        var result = await repository.GetByIdAsync(customerId);

        result.Should().NotBeNull();
        result!.Id.Should().Be(customerId);
        result.Email.Should().Be("alex@cleanarchitecture.xyz");
        result.Name.Should().Be("alex");
        result.Address.Should().Be("689 Cherry Hill Rd.Brooklyn, NY 11211");
    }
    
    [Fact]
    public async Task GetById_ByCustomerId_ShouldNotFoundError()
    {
        var customerId = new CustomerId(Guid.Empty);
        var repository = new CustomerReadRepository(_fixture.Context!);

        var result = await repository.GetByIdAsync(customerId);

        result.Should().BeNull();
    }
}