using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.OrderLines.Queries.GetOrderLines;

public sealed class GetOrderLinesValidator : AbstractValidator<GetOrderLinesQuery>
{
    public GetOrderLinesValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Orders.IdIsRequired);
    } 
}