using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.OrderLines;
using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Entites.Products;
using FluentResults;

namespace CleanArchitecture.Application.Core.OrderLines.Commands.UpdateOrderLine;

public record UpdateOrderLineCommand : ICommand<Result>
{
    public required OrderLineId OrderLineId { get; init; }
    public required OrderId OrderId { get; init; }
    public required ProductId ProductId { get; init; }
    public required int Quantity { get; init; }
}