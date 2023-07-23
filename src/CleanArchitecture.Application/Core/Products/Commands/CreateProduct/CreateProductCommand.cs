using CleanArchitecture.Application.Abstractions.Messaging;
using FluentResults;

namespace CleanArchitecture.Application.Core.Products.Commands.CreateProduct;

public record CreateProductCommand : ICommand<Result>
{
    public required string Name { get; init; }
    public required string Sku { get; init; }
    public required string Currency { get; init; }
    public required decimal Amount { get; init; }
}