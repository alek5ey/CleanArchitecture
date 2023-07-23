using CleanArchitecture.Application.Abstractions.Messaging;
using FluentResults;

namespace CleanArchitecture.Application.Core.Customers.Commands.CreateCustomer;

public record CreateCustomerCommand : ICommand<Result>
{
    public required string Email { get; init; }
    public required string Name { get; init; }
    public string? Address { get; init; }
}