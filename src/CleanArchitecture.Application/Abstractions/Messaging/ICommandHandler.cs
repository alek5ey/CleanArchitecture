using MediatR;

namespace CleanArchitecture.Application.Abstractions.Messaging;

internal interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
{
}