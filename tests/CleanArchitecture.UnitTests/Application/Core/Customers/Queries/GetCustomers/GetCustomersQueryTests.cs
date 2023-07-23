using CleanArchitecture.Application.Contracts.Common.Pagination;
using CleanArchitecture.Application.Core.Customers.Queries.GetCustomers;

namespace CleanArchitecture.UnitTests.Application.Core.Customers.Queries.GetCustomers;

public class GetCustomersQueryTests
{
    [Fact]
    public void Handle_WithParams_ShouldSuccessful()
    {
        var faker = new Faker();
        var email = faker.Person.Email;
        var name = faker.Person.FullName;
        var address = faker.Address.FullAddress();
        var page = faker.Random.Int(1, 100);
        var pageSize = faker.Random.Int(10, PaginatableRequest.MaxPageSize);
        var orderBy = "email.desc";

        var result = new GetCustomersQuery(
            email,
            name,
            address,
            page,
            pageSize,
            orderBy
        );
        
        result.Email.Should().Be(email.ToLower());
        result.Name.Should().Be(name.ToLower());
        result.Address.Should().Be(address.ToLower());
        result.Page.Should().Be(page);
        result.PageSize.Should().Be(pageSize);
        result.OrderBy.Should().Be("Email desc");
    }
}