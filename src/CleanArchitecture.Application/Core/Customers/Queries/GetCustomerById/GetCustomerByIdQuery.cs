using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.Customers.Responses;
using CleanArchitecture.Domain.Entites.Customers;
using FluentResults;

namespace CleanArchitecture.Application.Core.Customers.Queries.GetCustomerById;

public record GetCustomerByIdQuery(CustomerId CustomerId) : IQuery<Result<CustomerResponse>>;