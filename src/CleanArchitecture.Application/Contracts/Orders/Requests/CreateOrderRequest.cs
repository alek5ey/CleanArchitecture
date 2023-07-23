namespace CleanArchitecture.Application.Contracts.Orders.Requests;

public record CreateOrderRequest
{
    public required Guid CustomerId { get; init; }
    public required string? Comment { get; init; }
}