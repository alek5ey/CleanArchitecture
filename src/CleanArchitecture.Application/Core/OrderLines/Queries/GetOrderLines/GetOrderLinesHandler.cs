using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.OrderLines.Responses;
using CleanArchitecture.Domain.Repositories.OrderLines;
using CleanArchitecture.Domain.Repositories.Orders;
using FluentResults;

namespace CleanArchitecture.Application.Core.OrderLines.Queries.GetOrderLines;

internal sealed class GetOrderLinesHandler : IQueryHandler<GetOrderLinesQuery, Result<IReadOnlyList<OrderLineResponse>>>
{
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IOrderLineReadRepository _orderLineReadRepository;

    public GetOrderLinesHandler(
        IOrderReadRepository orderReadRepository,
        IOrderLineReadRepository orderLineReadRepository)
    {
        _orderReadRepository = orderReadRepository;
        _orderLineReadRepository = orderLineReadRepository;
    }

    public async Task<Result<IReadOnlyList<OrderLineResponse>>> Handle(GetOrderLinesQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderReadRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
            return Result.Fail(Exceptions.ConstantErrors.Orders.NotFoundById.WithMetadata("Id", request.OrderId));
        
        var orderLines = await _orderLineReadRepository.GetByOrderIdAsync(request.OrderId, cancellationToken);

        return orderLines
            .Select(ol => new OrderLineResponse
            {
                Id = ol.Id.Id,
                OrderId = ol.OrderId.Id,
                ProductId = ol.ProductId.Id,
                Currency =  ol.Price.Currency,
                Amount = ol.Price.Amount,
                Quantity = ol.Quantity,
                TotalPrice = ol.TotalPrice
            })
            .ToList();
    }
}