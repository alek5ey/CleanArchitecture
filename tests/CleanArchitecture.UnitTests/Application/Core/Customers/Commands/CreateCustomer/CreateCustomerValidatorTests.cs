using CleanArchitecture.Application.Core.Customers.Commands.CreateCustomer;

namespace CleanArchitecture.UnitTests.Application.Core.Customers.Commands.CreateCustomer;

public class CreateCustomerValidatorTests : IClassFixture<CreateCustomerValidator>
{
    private readonly CreateCustomerValidator _validator;
    
    public CreateCustomerValidatorTests(CreateCustomerValidator validator)
    {
        _validator = validator;
    }
    
    [Fact]
    public void Validate_WithCommand_ShouldSuccessful()
    {
        var command = new Faker<CreateCustomerCommand>()
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Address, f => f.Address.FullAddress());
        
        var result = _validator.TestValidate(command);
        
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("not_email_format")]
    public void Validate_WithCommand_ShouldEmailError(string email)
    {
        var command = new Faker<CreateCustomerCommand>()
            .RuleFor(c => c.Email, _ => email)
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Address, f => f.Address.FullAddress());
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Fact]
    public void Validate_WithCommand_ShouldNameError()
    {
        var command = new Faker<CreateCustomerCommand>()
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Name, f => string.Empty)
            .RuleFor(c => c.Address, f => f.Address.FullAddress());
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}