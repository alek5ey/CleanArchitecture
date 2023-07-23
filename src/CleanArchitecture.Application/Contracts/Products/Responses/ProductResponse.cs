namespace CleanArchitecture.Application.Contracts.Products.Responses;

public record ProductResponse
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Sku { get; init; }
    public required string Currency { get; init; }
    public required decimal Amount { get; init; }
}