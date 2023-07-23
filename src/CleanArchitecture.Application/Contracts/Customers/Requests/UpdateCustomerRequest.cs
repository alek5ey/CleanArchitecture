namespace CleanArchitecture.Application.Contracts.Customers.Requests;

public record UpdateCustomerRequest
{
    public required string Email { get; init; }
    public required string Name { get; init; }
    public required string? Address { get; init; }
}