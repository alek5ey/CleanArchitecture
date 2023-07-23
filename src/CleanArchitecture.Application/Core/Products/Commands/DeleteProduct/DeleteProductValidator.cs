using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Products.Commands.DeleteProduct;

public sealed class DeleteProductValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Products.IdIsRequired);
    } 
}