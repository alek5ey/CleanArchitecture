namespace CleanArchitecture.Application.Exceptions.ConstantErrors;

internal static class Pagination
{
    internal static ValidationError PageMustGreaterThanZero =>
        new($"{nameof(Pagination)}.{nameof(PageMustGreaterThanZero)}", "The page number must be greater than zero.");
    
    internal static ValidationError PageSizeMustGreaterThanZero =>
        new($"{nameof(Pagination)}.{nameof(PageSizeMustGreaterThanZero)}", "The page size number must be greater than zero.");
}