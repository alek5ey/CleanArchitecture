using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.Products.Responses;
using CleanArchitecture.Domain.Repositories.Products;
using FluentResults;

namespace CleanArchitecture.Application.Core.Products.Queries.GetProductById;

internal sealed class GetProductByIdQueryHandler : IQueryHandler<GetProductByIdQuery, Result<ProductResponse>>
{
    private readonly IProductReadRepository _productReadRepository;

    public GetProductByIdQueryHandler(IProductReadRepository productReadRepository)
    {
        _productReadRepository = productReadRepository;
    }

    public async Task<Result<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            return Result.Fail(Exceptions.ConstantErrors.Customers.NotFoundById.WithMetadata("Id", request.ProductId));

        return new ProductResponse
        {
            Id = product.Id.Id,
            Name = product.Name,
            Sku = product.Sku.Value,
            Currency = product.Money.Currency,
            Amount = product.Money.Amount
        };
    }
}