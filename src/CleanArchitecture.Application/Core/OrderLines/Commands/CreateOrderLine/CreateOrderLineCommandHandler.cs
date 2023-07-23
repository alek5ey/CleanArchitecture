using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Repositories.OrderLines;
using CleanArchitecture.Domain.Repositories.Orders;
using CleanArchitecture.Domain.Repositories.Products;
using FluentResults;

namespace CleanArchitecture.Application.Core.OrderLines.Commands.CreateOrderLine;

internal sealed class CreateOrderLineCommandHandler : ICommandHandler<CreateOrderLineCommand, Result>
{
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IOrderLineWriteRepository _orderLineWriteRepository;

    public CreateOrderLineCommandHandler(
        IOrderReadRepository orderReadRepository,
        IProductReadRepository productReadRepository,
        IOrderLineWriteRepository orderLineWriteRepository)
    {
        _orderReadRepository = orderReadRepository;
        _productReadRepository = productReadRepository;
        _orderLineWriteRepository = orderLineWriteRepository;
    }

    public async Task<Result> Handle(CreateOrderLineCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderReadRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
            return Result.Fail(Exceptions.ConstantErrors.Orders.NotFoundById.WithMetadata("Id", request.OrderId));
        
        var product = await _productReadRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            return Result.Fail(Exceptions.ConstantErrors.Products.NotFoundById.WithMetadata("Id", request.ProductId));
        
        if (order.OrderLines.Any(ol => ol.ProductId == product.Id))
            return Result.Fail(Exceptions.ConstantErrors.Orders.OrderAlreadyContainsProduct.WithMetadata("Id", request.ProductId));
        
        var orderLine = OrderLine.Create(
            new(Guid.NewGuid()),
            request.OrderId,
            request.ProductId,
            product.Money,
            request.Quantity);
        
        await _orderLineWriteRepository.AddAsync(orderLine, cancellationToken);
        
        return Result.Ok();
    }
}