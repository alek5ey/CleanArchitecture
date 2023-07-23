using System.Linq.Dynamic.Core;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.Common.Pagination;
using CleanArchitecture.Application.Contracts.Orders.Responses;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Domain.Repositories.Orders;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Core.Orders.Queries.GetOrders;

internal sealed class GetOrdersQueryHandler : IQueryHandler<GetOrdersQuery, Result<PagedResponse<OrderResponse>>>
{
    private readonly IOrderReadRepository _orderReadRepository;

    public GetOrdersQueryHandler(IOrderReadRepository orderReadRepository)
    {
        _orderReadRepository = orderReadRepository;
    }

    public async Task<Result<PagedResponse<OrderResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
    {
        var query = _orderReadRepository.Query
            .Include(o => o.OrderLines)
            .Where(o =>
                (request.CustomerId == null || o.CustomerId == request.CustomerId) &&
                (string.IsNullOrWhiteSpace(request.Comment) || (!string.IsNullOrWhiteSpace(o.Comment) && o.Comment.ToLower().Contains(request.Comment))))
            .OrderBy(request.OrderBy)
            .Select(o => new OrderResponse
            {
                Id = o.Id.Id,
                CustomerId = o.CustomerId.Id,
                CreateTime = o.CreateTime,
                LinesCount = o.OrderLines.Count,
                TotalPrice = o.TotalPrice,
                Comment = o.Comment
            });
        
        var page = await query.ToPage(request, cancellationToken);
        return page;
    }
}