using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.Orders;
using FluentResults;

namespace CleanArchitecture.Application.Core.Orders.Commands.DeleteOrder;

public record DeleteOrderCommand(OrderId OrderId) : ICommand<Result>;