using CleanArchitecture.Application.Contracts.Common.Pagination;

namespace CleanArchitecture.UnitTests.Application.Contracts.Common.Pagination;

public class PaginatableRequestTests
{
    [Fact]
    public void Create_WithParams_ShouldSuccessful()
    {
        var page = 1;
        var pageSize = 10;
        var orderBy = "name.desc";
        var defaultOrderBy = "Name";
        var mappedOrderByColumns = new Dictionary<string, string> { ["name"] = "Name" };

        var result = new FakePaginatableRequest(
            page,
            pageSize,
            orderBy,
            defaultOrderBy,
            mappedOrderByColumns);
        
        result.Page.Should().Be(page);
        result.PageSize.Should().Be(pageSize);
        result.OrderBy.Should().Be("Name desc");
    }
    
    [Theory]
    [InlineData(-1, -1)]
    [InlineData(0, 0)]
    public void Create_WithParams_ShouldOverrideIncorrectSuccessful(int page, int pageSize)
    {
        var result = new FakePaginatableRequest(
            page,
            pageSize,
            string.Empty,
            string.Empty,
            new());
        
        result.Page.Should().Be(PaginatableRequest.DefaultPage);
        result.PageSize.Should().Be(PaginatableRequest.DefaultPageSize);
    }
    
    [Fact]
    public void Create_WithParams_ShouldOverflowPageSizeSuccessful()
    {
        var result = new FakePaginatableRequest(
            PaginatableRequest.DefaultPage,
            PaginatableRequest.MaxPageSize + 1,
            string.Empty,
            string.Empty,
            new());
        
        result.PageSize.Should().Be(PaginatableRequest.MaxPageSize);
    }
    
    private record FakePaginatableRequest : PaginatableRequest
    {
        public FakePaginatableRequest(
            int page,
            int pageSize,
            string? orderBy,
            string defaultOrderBy,
            Dictionary<string, string> mappedOrderByColumns)
            : base(page, pageSize, orderBy, defaultOrderBy, mappedOrderByColumns)
        {
        }
    }
}