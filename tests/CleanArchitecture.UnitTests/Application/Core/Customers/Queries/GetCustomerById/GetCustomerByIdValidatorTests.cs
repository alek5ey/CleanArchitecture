using CleanArchitecture.Application.Core.Customers.Queries.GetCustomerById;
using CleanArchitecture.Domain.Entites.Customers;

namespace CleanArchitecture.UnitTests.Application.Core.Customers.Queries.GetCustomerById;

public class GetCustomerByIdValidatorTests : IClassFixture<GetCustomerByIdValidator>
{
    private readonly GetCustomerByIdValidator _validator;
    
    public GetCustomerByIdValidatorTests(GetCustomerByIdValidator validator)
    {
        _validator = validator;
    }
    
    [Fact]
    public void Validate_WithQuery_ShouldSuccessful()
    {
        var customerId = new CustomerId(Guid.NewGuid());
        var command = new GetCustomerByIdQuery(customerId);
        
        var result = _validator.TestValidate(command);
        
        result.ShouldNotHaveValidationErrorFor(x => x.CustomerId);
    }
    
    [Fact]
    public void Validate_WithQuery_ShouldError()
    {
        var command = new GetCustomerByIdQuery(null!);
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }
}