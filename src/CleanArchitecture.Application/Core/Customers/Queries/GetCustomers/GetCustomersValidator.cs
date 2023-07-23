using CleanArchitecture.Application.Core.Common.Validators;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Customers.Queries.GetCustomers;

public sealed class GetCustomersValidator : AbstractValidator<GetCustomersQuery>
{
    public GetCustomersValidator()
    {
        Include(new PaginationValidator());
    } 
}