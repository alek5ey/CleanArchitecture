using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Entites.OrderLines;
using FluentResults;

namespace CleanArchitecture.Application.Core.OrderLines.Commands.DeleteOrderLine;

public record DeleteOrderLineCommand(OrderLineId OrderLineId) : ICommand<Result>;