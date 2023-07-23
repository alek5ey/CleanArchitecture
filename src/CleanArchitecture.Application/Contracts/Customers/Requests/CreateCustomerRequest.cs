namespace CleanArchitecture.Application.Contracts.Customers.Requests;

public record CreateCustomerRequest
{
    public required string Email { get; init; }
    public required string Name { get; init; }
    public required string? Address { get; init; }
}