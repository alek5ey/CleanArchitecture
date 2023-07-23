using CleanArchitecture.Application.Exceptions;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Extensions;

internal static class ActionResultExtensions
{
    public static async Task<IActionResult> Match<T>(this Task<Result<T>> resultTask, Func<T, IActionResult> onSuccess, Func<IList<IError>, IActionResult> onFailure)
    {
        var result = await resultTask;
        
        return result switch
        {
            { IsSuccess: true } => onSuccess(result.Value),
            _ => onFailure(result.Errors)
        };
    }
    
    public static async Task<IActionResult> Match(this Task<Result> resultTask, Func<IActionResult> onSuccess, params Func<IList<IError>, IActionResult>[] onFailures)
    {
        var result = await resultTask;

        if (result.IsSuccess)
            return onSuccess();

        var notFoundFailure = onFailures.FirstOrDefault(f => f.Method.Name =="NotFound");
        if (result.Errors.Any(e => e is NotFoundError) && notFoundFailure is not null)
            return notFoundFailure(result.Errors);
        
        return onFailures.First()(result.Errors);
    }
}