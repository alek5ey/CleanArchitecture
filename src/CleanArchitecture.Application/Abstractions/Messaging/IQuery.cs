using MediatR;

namespace CleanArchitecture.Application.Abstractions.Messaging;

internal interface IQuery<out TResponse> : IRequest<TResponse>, IPipelineRequest
{
}