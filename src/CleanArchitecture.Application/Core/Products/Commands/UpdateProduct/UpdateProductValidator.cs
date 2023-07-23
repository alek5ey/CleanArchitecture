using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Domain.Entites.Products;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Products.Commands.UpdateProduct;

public sealed class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Products.IdIsRequired);
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Products.NameIsRequired);
        
        RuleFor(x => x.Currency)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Products.CurrencyNameIsRequired);
        
        RuleFor(x => x.Sku)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Products.SkuIdIsRequired);
        
        RuleFor(x => x.Sku)
            .Must(s => s.Length == Sku.DefaultLength)
            .WithError(Exceptions.ConstantErrors.Products.SkuMustBeValid);

        RuleFor(x => x.Amount)
            .GreaterThan(0)
            .WithError(Exceptions.ConstantErrors.Products.AmountIsGreatZero);
    } 
}