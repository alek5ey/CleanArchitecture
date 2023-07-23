using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.Orders.Responses;
using CleanArchitecture.Domain.Entites.Orders;
using FluentResults;

namespace CleanArchitecture.Application.Core.Orders.Queries.GetOrderById;

public record GetOrderByIdQuery(OrderId OrderId) : IQuery<Result<OrderResponse>>;