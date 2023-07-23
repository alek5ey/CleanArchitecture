namespace CleanArchitecture.Application.Contracts.OrderLines.Requests;

public record UpdateOrderLineRequest
{
    public required Guid OrderId { get; init; }
    public required Guid ProductId { get; init; }
    public required int Quantity { get; init; }
}