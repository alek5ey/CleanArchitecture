using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Customers.Commands.CreateCustomer;

public sealed class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Customers.EmailIsRequired);
        RuleFor(x => x.Email)
            .EmailAddress()
            .WithError(Exceptions.ConstantErrors.Customers.EmailIsInvalid);
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Customers.NameIsRequired);
    } 
}