using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Customers.Commands.UpdateCustomer;

public sealed class UpdateCustomerValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Customers.IdIsRequired);
        
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