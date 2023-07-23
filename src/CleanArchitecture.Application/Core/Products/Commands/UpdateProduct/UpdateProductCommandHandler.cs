using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.Products;
using CleanArchitecture.Domain.Repositories.Products;
using FluentResults;

namespace CleanArchitecture.Application.Core.Products.Commands.UpdateProduct;

internal sealed class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, Result>
{
    private readonly IProductReadRepository _productReadRepository;
    private readonly IProductWriteRepository _productWriteRepository;

    public UpdateProductCommandHandler(
        IProductReadRepository productReadRepository,
        IProductWriteRepository productWriteRepository)
    {
        _productReadRepository = productReadRepository;
        _productWriteRepository = productWriteRepository;
    }

    public async Task<Result> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _productReadRepository.GetByIdAsync(request.ProductId, cancellationToken);
        if (product is null)
            return Result.Fail(Exceptions.ConstantErrors.Products.NotFoundById.WithMetadata("Id", request.ProductId));
        
        product.Update(
            request.Name,
            Money.Create(request.Currency, request.Amount),
            Sku.Create(request.Sku));

        _productWriteRepository.Update(product);
        
        return Result.Ok();
    }
}