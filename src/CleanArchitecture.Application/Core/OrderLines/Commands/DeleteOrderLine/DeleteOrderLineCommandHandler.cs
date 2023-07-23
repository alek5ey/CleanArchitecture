using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Repositories.OrderLines;
using FluentResults;

namespace CleanArchitecture.Application.Core.OrderLines.Commands.DeleteOrderLine;

internal sealed class DeleteOrderLineCommandHandler : ICommandHandler<DeleteOrderLineCommand, Result>
{
    private readonly IOrderLineReadRepository _orderLineReadRepository;
    private readonly IOrderLineWriteRepository _orderLineWriteRepository;

    public DeleteOrderLineCommandHandler(
        IOrderLineReadRepository orderLineReadRepository,
        IOrderLineWriteRepository orderLineWriteRepository)
    {
        _orderLineReadRepository = orderLineReadRepository;
        _orderLineWriteRepository = orderLineWriteRepository;
    }

    public async Task<Result> Handle(DeleteOrderLineCommand request, CancellationToken cancellationToken)
    {
        var orderLine = await _orderLineReadRepository.GetByIdAsync(request.OrderLineId, cancellationToken);
        if (orderLine is null)
            return Result.Fail(Exceptions.ConstantErrors.OrderLines.NotFoundById.WithMetadata("Id", request.OrderLineId));
        
        orderLine.Delete();
        
        _orderLineWriteRepository.Remove(orderLine);
        
        return Result.Ok();
    }
}