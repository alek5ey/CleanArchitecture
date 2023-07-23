using CleanArchitecture.Application.Contracts.Common.Pagination;
using CleanArchitecture.Domain.Entites.Customers;

namespace CleanArchitecture.UnitTests.Application.Contracts.Common.Pagination;

public class PagedResponseTests
{
    [Fact]
    public void Create_WithParams_ShouldSuccessful()
    {
        var customersPageCount = 10;
        var totalCount = 100;
        var page = 1;
        var customers = new Faker<Customer>()
            .CustomInstantiator(f => Customer.Create(new CustomerId(Guid.NewGuid()), f.Person.Email, f.Person.FullName, f.Address.FullAddress()))
            .Generate(customersPageCount);

        var result = new PagedResponse<Customer>(customers, totalCount, page, customersPageCount);

        result.Should().BeOfType(typeof(PagedResponse<Customer>));
        result.TotalCount.Should().Be(totalCount);
        result.Page.Should().Be(page);
        result.PageSize.Should().Be(customersPageCount);
        result.TotalPage.Should().Be(10);
        result.HasNextPage.Should().BeTrue();
        result.HasPreviousPage.Should().BeFalse();
        result.Items.Should().Equal(customers);
    }
}