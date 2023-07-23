using System.Net;
using CleanArchitecture.Domain.Primitives;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace CleanArchitecture.Web.Helpers;

internal static class ApiErrorResponseBuilder
{
    public static ValidationProblemDetails Build(
        HttpContext httpContext,
        HttpStatusCode statusCode,
        IEnumerable<IError>? errors = null,
        Exception? exception = null)
    {
        var errorsDict = new Dictionary<string, string[]>();

        if (errors != null)
        {
            errorsDict = errors
                .Cast<DomainError>()
                .GroupBy(e => e.Code)
                .ToDictionary(g => g.Key,
                    g => g.Select(e => e.Message).ToArray());
        }

        if (exception != null)
            errorsDict.TryAdd("System.Error", new[] { exception.Message });
        
        return new (errorsDict)
        {
            Title = ReasonPhrases.GetReasonPhrase((int)statusCode),
            Status = (int)statusCode,
            Type = $"https://httpstatuses.io/{(int)statusCode}",
            Instance = httpContext.Request.Path,
            Extensions =
            {
                ["traceId"] = httpContext.TraceIdentifier
            }
        };
    }
}