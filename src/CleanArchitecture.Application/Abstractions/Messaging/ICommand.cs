using MediatR;

namespace CleanArchitecture.Application.Abstractions.Messaging;

internal interface ICommand<out TResponse> : IRequest<TResponse>, IPipelineRequest
{
}