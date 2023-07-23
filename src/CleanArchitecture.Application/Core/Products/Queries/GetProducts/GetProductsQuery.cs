using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.Common.Pagination;
using CleanArchitecture.Application.Contracts.Products.Responses;
using CleanArchitecture.Domain.Entites.Products;
using FluentResults;

namespace CleanArchitecture.Application.Core.Products.Queries.GetProducts;

public record GetProductsQuery : PaginatableRequest, IQuery<Result<PagedResponse<ProductResponse>>>
{
    public string? Name { get; }
    public string? Sku { get; }
    public int? Amount { get; }

    private static readonly string DefaultOrderBy = nameof(Name);
    private static readonly Dictionary<string, string> MappedOrderByColumns = new()
    {
        [nameof(Name).ToLower()] = nameof(Name),
        [nameof(Sku).ToLower()] = nameof(Sku),
        [nameof(Amount).ToLower()] = $"{nameof(Money)}.{nameof(Money.Amount)}"
    };

    public GetProductsQuery(
        string? name,
        string? sku,
        int? amount,
        int page,
        int pageSize,
        string? orderBy)
        : base(page, pageSize, orderBy, DefaultOrderBy, MappedOrderByColumns)
    {
        Name = name?.ToLower();
        Sku = sku?.ToLower();
        Amount = amount;
    }
}