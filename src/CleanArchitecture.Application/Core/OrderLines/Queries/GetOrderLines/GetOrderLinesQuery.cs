using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.OrderLines.Responses;
using CleanArchitecture.Domain.Entites.Orders;
using FluentResults;

namespace CleanArchitecture.Application.Core.OrderLines.Queries.GetOrderLines;

public record GetOrderLinesQuery(OrderId OrderId) : IQuery<Result<IReadOnlyList<OrderLineResponse>>>;