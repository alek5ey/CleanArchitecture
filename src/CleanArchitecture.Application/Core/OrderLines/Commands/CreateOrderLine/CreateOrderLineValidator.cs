using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.OrderLines.Commands.CreateOrderLine;

public sealed class CreateOrderLineValidator : AbstractValidator<CreateOrderLineCommand>
{
    public CreateOrderLineValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Orders.IdIsRequired);
        
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Products.IdIsRequired);
        
        RuleFor(x => x.Quantity)
            .GreaterThanOrEqualTo(1)
            .WithError(Exceptions.ConstantErrors.OrderLines.QuantityNotValid);
    } 
}