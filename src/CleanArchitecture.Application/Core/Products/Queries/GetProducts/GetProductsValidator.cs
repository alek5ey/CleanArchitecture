using CleanArchitecture.Application.Core.Common.Validators;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Products.Queries.GetProducts;

public sealed class GetProductsValidator : AbstractValidator<GetProductsQuery>
{
    public GetProductsValidator()
    {
        Include(new PaginationValidator());
    } 
}