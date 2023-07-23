using CleanArchitecture.Application.Core.Customers.Commands.UpdateCustomer;

namespace CleanArchitecture.UnitTests.Application.Core.Customers.Commands.UpdateCustomer;

public class UpdateCustomerValidatorTests : IClassFixture<UpdateCustomerValidator>
{
    private readonly UpdateCustomerValidator _validator;
    
    public UpdateCustomerValidatorTests(UpdateCustomerValidator validator)
    {
        _validator = validator;
    }
    
    [Fact]
    public void Validate_WithCommand_ShouldSuccessful()
    {
        var command = new Faker<UpdateCustomerCommand>()
            .RuleFor(c => c.CustomerId, _ => new(Guid.NewGuid()))
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Address, f => f.Address.FullAddress());
        
        var result = _validator.TestValidate(command);
        
        result.ShouldNotHaveValidationErrorFor(x => x.CustomerId);
        result.ShouldNotHaveValidationErrorFor(x => x.Email);
        result.ShouldNotHaveValidationErrorFor(x => x.Name);
    }
    
    [Fact]
    public void Validate_WithCommand_ShouldCustomerIdError()
    {
        var command = new Faker<UpdateCustomerCommand>()
            .RuleFor(c => c.CustomerId, _ => null!)
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Address, f => f.Address.FullAddress());
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.CustomerId);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("not_email_format")]
    public void Validate_WithCommand_ShouldEmailError(string email)
    {
        var command = new Faker<UpdateCustomerCommand>()
            .RuleFor(c => c.CustomerId, _ => new(Guid.NewGuid()))
            .RuleFor(c => c.Email, _ => email)
            .RuleFor(c => c.Name, f => f.Person.FullName)
            .RuleFor(c => c.Address, f => f.Address.FullAddress());
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Email);
    }
    
    [Fact]
    public void Validate_WithCommand_ShouldNameError()
    {
        var command = new Faker<UpdateCustomerCommand>()
            .RuleFor(c => c.CustomerId, _ => new(Guid.NewGuid()))
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Name, f => string.Empty)
            .RuleFor(c => c.Address, f => f.Address.FullAddress());
        
        var result = _validator.TestValidate(command);
        
        result.ShouldHaveValidationErrorFor(x => x.Name);
    }
}