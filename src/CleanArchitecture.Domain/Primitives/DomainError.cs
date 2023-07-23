using FluentResults;

namespace CleanArchitecture.Domain.Primitives;

public abstract class DomainError : Error
{
    public string Code => Metadata.GetValueOrDefault("ErrorCode", "unknown").ToString()!;

    protected DomainError(string code, string message) : base(message)
    {
        WithMetadata("ErrorCode", code);
    }
}