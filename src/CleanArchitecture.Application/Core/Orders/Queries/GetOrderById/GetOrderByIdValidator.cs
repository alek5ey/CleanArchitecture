using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Orders.Queries.GetOrderById;

public sealed class GetOrderByIdValidator : AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Orders.IdIsRequired);
    } 
}