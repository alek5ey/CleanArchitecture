using CleanArchitecture.Domain.Primitives;

namespace CleanArchitecture.Application.Exceptions;

public class ValidationError : DomainError
{
    public ValidationError(string code, string message)
        : base(code, message)
    {
    }
}