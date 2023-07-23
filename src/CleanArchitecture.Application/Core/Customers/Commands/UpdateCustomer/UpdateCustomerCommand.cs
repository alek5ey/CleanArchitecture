using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.Customers;
using FluentResults;

namespace CleanArchitecture.Application.Core.Customers.Commands.UpdateCustomer;

public record UpdateCustomerCommand : ICommand<Result>
{
    public required CustomerId CustomerId { get; init; }
    public required string Email { get; init; }
    public required string Name { get; init; }
    public string? Address { get; init; }
}