using System.Linq.Dynamic.Core;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.Common.Pagination;
using CleanArchitecture.Application.Contracts.Customers.Responses;
using CleanArchitecture.Application.Extensions;
using CleanArchitecture.Domain.Repositories.Customers;
using FluentResults;

namespace CleanArchitecture.Application.Core.Customers.Queries.GetCustomers;

internal sealed class GetCustomersQueryHandler : IQueryHandler<GetCustomersQuery, Result<PagedResponse<CustomerResponse>>>
{
    private readonly ICustomerReadRepository _customerReadRepository;

    public GetCustomersQueryHandler(ICustomerReadRepository customerReadRepository)
    {
        _customerReadRepository = customerReadRepository;
    }

    public async Task<Result<PagedResponse<CustomerResponse>>> Handle(GetCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var query = _customerReadRepository.Query
            .Where(c =>
                (string.IsNullOrWhiteSpace(request.Email) || c.Email.ToLower().Contains(request.Email)) &&
                (string.IsNullOrWhiteSpace(request.Name) || c.Name.ToLower().Contains(request.Name)) &&
                (string.IsNullOrWhiteSpace(request.Address) || (!string.IsNullOrWhiteSpace(c.Address) && c.Address.ToLower().Contains(request.Address))))
            .OrderBy(request.OrderBy)
            .Select(c => new CustomerResponse
            {
                Id = c.Id.Id,
                Email = c.Email,
                Name = c.Name,
                Address = c.Address
            });

        var page = await query.ToPage(request, cancellationToken);
        return page;
    }
}