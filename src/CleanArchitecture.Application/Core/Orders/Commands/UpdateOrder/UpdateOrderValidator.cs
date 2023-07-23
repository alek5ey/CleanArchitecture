using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Orders.Commands.UpdateOrder;

public sealed class UpdateOrderValidator : AbstractValidator<UpdateOrderCommand>
{
    public UpdateOrderValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Orders.NotFoundById);
        
        RuleFor(x => x.CustomerId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Customers.IdIsRequired);
    } 
}