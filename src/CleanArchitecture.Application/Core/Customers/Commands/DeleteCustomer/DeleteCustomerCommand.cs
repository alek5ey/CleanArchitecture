using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.Customers;
using FluentResults;

namespace CleanArchitecture.Application.Core.Customers.Commands.DeleteCustomer;

public record DeleteCustomerCommand(CustomerId CustomerId) : ICommand<Result>;