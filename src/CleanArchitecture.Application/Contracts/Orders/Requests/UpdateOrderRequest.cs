namespace CleanArchitecture.Application.Contracts.Orders.Requests;

public record UpdateOrderRequest
{
    public required Guid CustomerId { get; init; }
    public required string? Comment { get; init; }
}