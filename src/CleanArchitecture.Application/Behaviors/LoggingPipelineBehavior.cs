using CleanArchitecture.Application.Abstractions.Messaging;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using ApplicationException = CleanArchitecture.Application.Exceptions.ApplicationException;

namespace CleanArchitecture.Application.Behaviors;

internal sealed class LoggingPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IPipelineRequest
    where TResponse : ResultBase, new()
{
    private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> _logger;

    public LoggingPipelineBehavior(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation(
            "Start request {@RequestName}",
            typeof(TRequest).Name);

        var response = await next();

        if (response.IsSuccess)
        {
            _logger.LogInformation(
                "Successful request {@RequestName}",
                typeof(TRequest).Name);
        }
        else
        {
            var applicationException = new ApplicationException(typeof(TRequest).Name, response.Errors);
            
            _logger.LogError(
                applicationException,
                "Request failure {@RequestName}",
                typeof(TRequest).Name);
        }

        return response;
    }
}