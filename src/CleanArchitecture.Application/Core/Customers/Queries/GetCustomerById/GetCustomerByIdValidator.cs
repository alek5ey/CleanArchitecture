using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Customers.Queries.GetCustomerById;

public sealed class GetCustomerByIdValidator : AbstractValidator<GetCustomerByIdQuery>
{
    public GetCustomerByIdValidator()
    {
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Customers.IdIsRequired);
    } 
}