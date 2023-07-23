using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Repositories.OrderLines;
using CleanArchitecture.Domain.Repositories.Orders;
using FluentResults;

namespace CleanArchitecture.Application.Core.Orders.Commands.DeleteOrder;

internal sealed class DeleteOrderCommandHandler : ICommandHandler<DeleteOrderCommand, Result>
{
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly IOrderLineReadRepository _orderLineReadRepository;
    private readonly IOrderLineWriteRepository _orderLineWriteRepository;

    public DeleteOrderCommandHandler(
        IOrderReadRepository orderReadRepository,
        IOrderWriteRepository orderWriteRepository,
        IOrderLineReadRepository orderLineReadRepository,
        IOrderLineWriteRepository orderLineWriteRepository)
    {
        _orderReadRepository = orderReadRepository;
        _orderWriteRepository = orderWriteRepository;
        _orderLineReadRepository = orderLineReadRepository;
        _orderLineWriteRepository = orderLineWriteRepository;
    }

    public async Task<Result> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderReadRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
            return Result.Fail(Exceptions.ConstantErrors.Orders.NotFoundById.WithMetadata("Id", request.OrderId));
        
        order.Delete();
        
        foreach (var orderLine in order.OrderLines)
            _orderLineWriteRepository.Remove(orderLine);

        _orderWriteRepository.Remove(order);
        
        return Result.Ok();
    }
}