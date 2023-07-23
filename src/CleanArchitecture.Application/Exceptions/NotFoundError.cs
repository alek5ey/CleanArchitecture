using CleanArchitecture.Domain.Primitives;

namespace CleanArchitecture.Application.Exceptions;

public class NotFoundError : DomainError
{
    public NotFoundError(string code, string message)
        : base(code, message)
    {
    }
}