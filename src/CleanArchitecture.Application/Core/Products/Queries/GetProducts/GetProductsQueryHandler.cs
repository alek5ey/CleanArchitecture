using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.Common.Pagination;
using System.Linq.Dynamic.Core;
using CleanArchitecture.Application.Contracts.Products.Responses;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Domain.Repositories.Products;
using FluentResults;

namespace CleanArchitecture.Application.Core.Products.Queries.GetProducts;

internal sealed class GetProductsQueryHandler : IQueryHandler<GetProductsQuery, Result<PagedResponse<ProductResponse>>>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetProductsQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<Result<PagedResponse<ProductResponse>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        var query = _productReadRepository.Query
            .Where(p =>
                (string.IsNullOrWhiteSpace(request.Name) || p.Name.ToLower().Contains(request.Name)) &&
                (string.IsNullOrWhiteSpace(request.Sku) || p.Sku.Value.ToLower().Contains(request.Sku)) &&
                (!request.Amount.HasValue || p.Money.Amount == request.Amount))
            .OrderBy(request.OrderBy)
            .Select(p => new ProductResponse
            {
                Id = p.Id.Id,
                Name = p.Name,
                Sku = p.Sku.Value,
                Currency = p.Money.Currency,
                Amount = p.Money.Amount
            });
        
        var page = await query.ToPage(request, cancellationToken);
        return page;
    }
}