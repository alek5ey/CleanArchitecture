using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.Customers.Responses;
using CleanArchitecture.Domain.Repositories.Customers;
using FluentResults;

namespace CleanArchitecture.Application.Core.Customers.Queries.GetCustomerById;

internal sealed class GetCustomerByIdQueryHandler : IQueryHandler<GetCustomerByIdQuery, Result<CustomerResponse>>
{
    private readonly ICustomerReadRepository _customerReadRepository;

    public GetCustomerByIdQueryHandler(ICustomerReadRepository customerReadRepository)
    {
        _customerReadRepository = customerReadRepository;
    }

    public async Task<Result<CustomerResponse>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        var customer = await _customerReadRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
            return Result.Fail(Exceptions.ConstantErrors.Customers.NotFoundById.WithMetadata("Id", request.CustomerId));

        return new CustomerResponse
        {
            Id = customer.Id.Id,
            Email = customer.Email,
            Name = customer.Name,
            Address = customer.Address
        };
    }
}