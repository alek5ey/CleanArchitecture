using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.OrderLines.Commands.UpdateOrderLine;

public sealed class UpdateOrderLineValidator : AbstractValidator<UpdateOrderLineCommand>
{
    public UpdateOrderLineValidator()
    {
        RuleFor(x => x.OrderLineId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.OrderLines.IdIsRequired);
        
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