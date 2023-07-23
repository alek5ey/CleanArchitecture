using System.Text;
using CleanArchitecture.Domain.Primitives;
using FluentResults;

namespace CleanArchitecture.Application.Exceptions;

public class ApplicationException : Exception
{
    private readonly string _methodName;
    private readonly IReadOnlyCollection<DomainError> _domainErrors;
    public override string StackTrace => Environment.StackTrace;

    public ApplicationException(string methodName, IEnumerable<IError> failures)
        :base("One or more request failures has occurred.")
    {
        _methodName = methodName;
        _domainErrors = failures
            .Distinct()
            .Cast<DomainError>()
            .ToList();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.AppendLine($"{nameof(ApplicationException)}: {_methodName}() - {Message}");

        foreach (var domainError in _domainErrors)
            sb.AppendLine(domainError.ToString());

        // sb.AppendLine(StackTrace);
        
        return sb.ToString();
    }
}