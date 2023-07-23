using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.Products;
using FluentResults;

namespace CleanArchitecture.Application.Core.Products.Commands.UpdateProduct;

public record UpdateProductCommand : ICommand<Result>
{
    public required ProductId ProductId { get; init; }
    public required string Name { get; init; }
    public required string Sku { get; init; }
    public required string Currency { get; init; }
    public required decimal Amount { get; init; }
}