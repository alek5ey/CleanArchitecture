using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Repositories.OrderLines;
using CleanArchitecture.Domain.Repositories.Orders;
using CleanArchitecture.Domain.Repositories.Products;
using FluentResults;

namespace CleanArchitecture.Application.Core.OrderLines.Commands.UpdateOrderLine;

internal sealed class UpdateOrderLineCommandHandler : ICommandHandler<UpdateOrderLineCommand, Result>
{
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IOrderLineReadRepository _orderLineReadRepository;
    private readonly IOrderLineWriteRepository _orderLineWriteRepository;

    public UpdateOrderLineCommandHandler(
        IOrderReadRepository orderReadRepository,
        IProductReadRepository productReadRepository,
        IOrderLineReadRepository orderLineReadRepository,
        IOrderLineWriteRepository orderLineWriteRepository)
    {
        _orderReadRepository = orderReadRepository;
        _productReadRepository = productReadRepository;
        _orderLineReadRepository = orderLineReadRepository;
        _orderLineWriteRepository = orderLineWriteRepository;
    }

    public async Task<Result> Handle(UpdateOrderLineCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderReadRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
            return Result.Fail(Exceptions.ConstantErrors.Orders.NotFoundById.WithMetadata("Id", request.OrderId));
        
        var product = await _productReadRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            return Result.Fail(Exceptions.ConstantErrors.Products.NotFoundById.WithMetadata("Id", request.ProductId));
        
        var orderLine = await _orderLineReadRepository.GetByIdAsync(request.OrderLineId, cancellationToken);
        if (orderLine is null)
            return Result.Fail(Exceptions.ConstantErrors.OrderLines.NotFoundById.WithMetadata("Id", request.OrderLineId));
        
        orderLine.Update(
            request.OrderId,
            request.ProductId,
            product.Money,
            request.Quantity);

        _orderLineWriteRepository.Update(orderLine);
        
        return Result.Ok();
    }
}