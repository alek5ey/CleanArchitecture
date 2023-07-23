using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.Common.Pagination;
using CleanArchitecture.Application.Contracts.Orders.Responses;
using CleanArchitecture.Domain.Entites.Customers;
using FluentResults;

namespace CleanArchitecture.Application.Core.Orders.Queries.GetOrders;

public record GetOrdersQuery : PaginatableRequest, IQuery<Result<PagedResponse<OrderResponse>>>
{
    public CustomerId? CustomerId { get; }
    public string? Comment { get; }

    private static readonly string DefaultOrderBy = "Id";
    private static readonly Dictionary<string, string> MappedOrderByColumns = new()
    {
        ["customerId"] = nameof(CustomerId),
        [nameof(Comment).ToLower()] = nameof(Comment)
    };

    public GetOrdersQuery(
        Guid? customerId,
        string? comment,
        int page,
        int pageSize,
        string? orderBy)
        : base(page, pageSize, orderBy, DefaultOrderBy, MappedOrderByColumns)
    {
        CustomerId = customerId.HasValue ? new(customerId.Value) : null;
        Comment = comment?.ToLower();
    }
}