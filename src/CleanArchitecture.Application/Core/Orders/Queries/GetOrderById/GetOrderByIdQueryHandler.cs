using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.Orders.Responses;
using CleanArchitecture.Domain.Repositories.OrderLines;
using CleanArchitecture.Domain.Repositories.Orders;
using FluentResults;

namespace CleanArchitecture.Application.Core.Orders.Queries.GetOrderById;

internal sealed class GetOrderByIdQueryHandler : IQueryHandler<GetOrderByIdQuery, Result<OrderResponse>>
{
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IOrderLineReadRepository _orderLineReadRepository;

    public GetOrderByIdQueryHandler(
        IOrderReadRepository orderReadRepository,
        IOrderLineReadRepository orderLineReadRepository)
    {
        _orderReadRepository = orderReadRepository;
        _orderLineReadRepository = orderLineReadRepository;
    }

    public async Task<Result<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderReadRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
            return Result.Fail(Exceptions.ConstantErrors.Orders.NotFoundById.WithMetadata("Id", request.OrderId));
        
        return new OrderResponse
        {
            Id = order.Id.Id,
            CustomerId = order.CustomerId.Id,
            CreateTime = order.CreateTime,
            LinesCount = order.OrderLines.Count,
            TotalPrice = order.TotalPrice,
            Comment = order.Comment
        };
    }
}