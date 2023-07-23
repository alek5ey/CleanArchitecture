using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.Products;
using CleanArchitecture.Domain.Repositories.Products;
using FluentResults;

namespace CleanArchitecture.Application.Core.Products.Commands.CreateProduct;

internal sealed class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, Result>
{
    private readonly IProductWriteRepository _productWriteRepository;

    public CreateProductCommandHandler(IProductWriteRepository productWriteRepository)
    {
        _productWriteRepository = productWriteRepository;
    }

    public async Task<Result> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = Product.Create(
            new(Guid.NewGuid()),
            request.Name,
            Money.Create(request.Currency, request.Amount),
            Sku.Create(request.Sku));
        
        await _productWriteRepository.AddAsync(product, cancellationToken);
        
        return Result.Ok();
    }
}