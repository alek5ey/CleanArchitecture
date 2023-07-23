using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Entites.Orders;
using FluentResults;

namespace CleanArchitecture.Application.Core.Orders.Commands.UpdateOrder;

public record UpdateOrderCommand : ICommand<Result>
{
    public required OrderId OrderId { get; init; }
    public required CustomerId CustomerId { get; init; }
    public string? Comment { get; init; }
}