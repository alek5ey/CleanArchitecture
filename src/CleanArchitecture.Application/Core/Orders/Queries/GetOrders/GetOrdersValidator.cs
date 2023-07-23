using CleanArchitecture.Application.Core.Common.Validators;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Orders.Queries.GetOrders;

public sealed class GetOrdersValidator : AbstractValidator<GetOrdersQuery>
{
    public GetOrdersValidator()
    {
        Include(new PaginationValidator());
    } 
}