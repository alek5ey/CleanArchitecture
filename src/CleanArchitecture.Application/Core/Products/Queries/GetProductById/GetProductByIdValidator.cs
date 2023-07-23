using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Products.Queries.GetProductById;

public sealed class GetProductByIdValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Products.IdIsRequired);
    } 
}