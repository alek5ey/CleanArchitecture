namespace CleanArchitecture.Application.Contracts.OrderLines.Responses;

public record OrderLineResponse
{
    public required Guid Id { get; init; }
    public required Guid OrderId { get; init; }
    public required Guid ProductId { get; init; }
    public required string Currency { get; init; }
    public required decimal Amount { get; init; }
    public required int Quantity { get; init; }
    public required decimal TotalPrice { get; init; }
}