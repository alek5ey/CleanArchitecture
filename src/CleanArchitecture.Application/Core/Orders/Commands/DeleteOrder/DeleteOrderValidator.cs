using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Orders.Commands.DeleteOrder;

public sealed class DeleteOrderValidator : AbstractValidator<DeleteOrderCommand>
{
    public DeleteOrderValidator()
    {
        RuleFor(x => x.OrderId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.Orders.IdIsRequired);
    } 
}