using CleanArchitecture.Application.Contracts.Common.Pagination;
using CleanArchitecture.Application.Core.Common.Validators;

namespace CleanArchitecture.UnitTests.Application.Core.Common.Validators;

public class PaginationValidatorTests : IClassFixture<PaginationValidator>
{
    private readonly PaginationValidator _validator;
    
    public PaginationValidatorTests(PaginationValidator validator)
    {
        _validator = validator;
    }
    
    [Theory]
    [InlineData(-10)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    public void Validate_WithRequest_ShouldPageSuccessful(int page)
    {
        var mockRequest = new Mock<PaginatableRequest>(
            page,
            10,
            string.Empty,
            string.Empty,
            new Dictionary<string, string>());
        
        var result = _validator.TestValidate(mockRequest.Object);
        
        result.ShouldNotHaveValidationErrorFor(x => x.Page);
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(10_000)]
    public void Validate_WithRequest_ShouldPageSizeSuccessful(int pageSize)
    {
        var mockRequest = new Mock<PaginatableRequest>(
            1,
            pageSize,
            string.Empty,
            string.Empty,
            new Dictionary<string, string>());
        
        var result = _validator.TestValidate(mockRequest.Object);
        
        result.ShouldNotHaveValidationErrorFor(x => x.PageSize);
    }
}