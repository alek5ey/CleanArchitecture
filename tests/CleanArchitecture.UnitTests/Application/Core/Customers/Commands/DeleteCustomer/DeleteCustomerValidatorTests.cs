using CleanArchitecture.Application.Core.Customers.Commands.DeleteCustomer;
using CleanArchitecture.Domain.Entites.Customers;

namespace CleanArchitecture.UnitTests.Application.Core.Customers.Commands.DeleteCustomer;

public class DeleteCustomerValidatorTests : IClassFixture<DeleteCustomerValidator>
{
    private readonly DeleteCustomerValidator _validator;
    
    public DeleteCustomerValidatorTests(DeleteCustomerValidator validator)
    {
        _validator = validator;
    }
    
    [Fact]
    public void Validate_WithCommand_ShouldSuccessful()
    {
        var customerId = new CustomerId(Guid.NewGuid());
        var command = new DeleteCustomerCommand(customerId);
        
        var result = _validator.TestValidate(command);
        
        result.ShouldNotHaveValidationErrorFor(x => x.CustomerId);
    }
    
    [Fact]
    public void Validate_WithCommand_ShouldError()
    {
        var command = new DeleteCustomerCommand(null!);
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }
}