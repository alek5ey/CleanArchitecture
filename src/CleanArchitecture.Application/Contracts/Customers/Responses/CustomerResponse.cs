namespace CleanArchitecture.Application.Contracts.Customers.Responses;

public record CustomerResponse
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string Name { get; init; }
    public required string? Address { get; init; }
}