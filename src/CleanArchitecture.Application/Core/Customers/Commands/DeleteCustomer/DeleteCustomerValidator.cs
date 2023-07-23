using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Customers.Commands.DeleteCustomer;

public sealed class DeleteCustomerValidator : AbstractValidator<DeleteCustomerCommand>
{
    public DeleteCustomerValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Customers.IdIsRequired);
    } 
}