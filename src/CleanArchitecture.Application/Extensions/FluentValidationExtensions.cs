using CleanArchitecture.Domain.Primitives;
using FluentValidation;

namespace CleanArchitecture.Application.Extensions;

internal static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, DomainError error)
    {
        if (error is null)
            throw new ArgumentNullException(nameof(error), "The error is required");

        return rule.WithMessage(error.Message);
    }
}