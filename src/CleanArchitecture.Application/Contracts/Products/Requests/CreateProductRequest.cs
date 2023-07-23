namespace CleanArchitecture.Application.Contracts.Products.Requests;

public record CreateProductRequest
{
    public required string Name { get; init; }
    public required string Sku { get; init; }
    public required string Currency { get; init; }
    public required decimal Amount { get; init; }
}