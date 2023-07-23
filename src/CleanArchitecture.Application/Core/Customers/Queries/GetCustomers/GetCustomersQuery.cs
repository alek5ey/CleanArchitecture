using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Application.Contracts.Common.Pagination;
using CleanArchitecture.Application.Contracts.Customers.Responses;
using FluentResults;

namespace CleanArchitecture.Application.Core.Customers.Queries.GetCustomers;

public record GetCustomersQuery : PaginatableRequest, IQuery<Result<PagedResponse<CustomerResponse>>>
{
    public string? Email { get; }
    public string? Name { get; }
    public string? Address { get; }

    private static readonly string DefaultOrderBy = nameof(Name);
    private static readonly Dictionary<string, string> MappedOrderByColumns = new()
    {
        [nameof(Email).ToLower()] = nameof(Email),
        [nameof(Name).ToLower()] = nameof(Name),
        [nameof(Address).ToLower()] = nameof(Address)
    };

    public GetCustomersQuery(
        string? email,
        string? name,
        string? address,
        int page,
        int pageSize,
        string? orderBy)
        : base(page, pageSize, orderBy, DefaultOrderBy, MappedOrderByColumns)
    {
        Email = email?.ToLower();
        Name = name?.ToLower();
        Address = address?.ToLower();
    }
}