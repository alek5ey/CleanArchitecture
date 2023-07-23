using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.OrderLines.Commands.DeleteOrderLine;

public sealed class DeleteOrderLineValidator : AbstractValidator<DeleteOrderLineCommand>
{
    public DeleteOrderLineValidator()
    {
        RuleFor(x => x.OrderLineId)
            .NotEmpty()
            .WithError(Exceptions.ConstantErrors.OrderLines.IdIsRequired);
    } 
}