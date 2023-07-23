namespace CleanArchitecture.Application.Contracts.Orders.Responses;

public record OrderResponse
{
    public required Guid Id { get; init; }
    public required Guid CustomerId { get; init; }
    public required DateTimeOffset CreateTime { get; init; }
    public required int LinesCount { get; init; }
    public required decimal TotalPrice { get; init; }
    public required string? Comment { get; init; }
}