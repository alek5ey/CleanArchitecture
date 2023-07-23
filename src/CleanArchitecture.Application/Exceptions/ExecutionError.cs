using CleanArchitecture.Domain.Primitives;

namespace CleanArchitecture.Application.Exceptions;

public class ExecutionError : DomainError
{
    public ExecutionError(string code, string message)
        : base(code, message)
    {
    }
}