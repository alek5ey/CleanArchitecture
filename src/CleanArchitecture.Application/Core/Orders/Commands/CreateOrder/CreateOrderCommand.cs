using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.Customers;
using FluentResults;

namespace CleanArchitecture.Application.Core.Orders.Commands.CreateOrder;

public record CreateOrderCommand : ICommand<Result>
{
    public required CustomerId CustomerId { get; init; }
    public string? Comment { get; init; }
}