using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Entites.Products;
using FluentResults;

namespace CleanArchitecture.Application.Core.OrderLines.Commands.CreateOrderLine;

public record CreateOrderLineCommand : ICommand<Result>
{
    public required OrderId OrderId { get; init; }
    public required ProductId ProductId { get; init; }
    public required int Quantity { get; init; }
}