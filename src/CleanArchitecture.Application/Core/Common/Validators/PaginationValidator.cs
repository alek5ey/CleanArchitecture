using CleanArchitecture.Application.Contracts.Common.Pagination;
using CleanArchitecture.Application.Exceptions.ConstantErrors;
using CleanArchitecture.Application.Extensions;
using FluentValidation;

namespace CleanArchitecture.Application.Core.Common.Validators;

public sealed class PaginationValidator : AbstractValidator<PaginatableRequest>
{
    public PaginationValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(0)
            .WithError(Pagination.PageMustGreaterThanZero);
        
        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(0)
            .WithError(Pagination.PageSizeMustGreaterThanZero);
    } 
}