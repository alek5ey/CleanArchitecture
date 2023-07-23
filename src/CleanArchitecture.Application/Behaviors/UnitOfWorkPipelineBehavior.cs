using System.Transactions;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using FluentResults;
using MediatR;

namespace CleanArchitecture.Application.Behaviors;

internal sealed class UnitOfWorkPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<ResultBase>
    where TResponse : ResultBase, new()
{
    private readonly IUnitOfWork _unitOfWork;

    public UnitOfWorkPipelineBehavior(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<TResponse> Handle(TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);
        
        var response = await next();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (response.IsSuccess)
        {
            transactionScope.Complete();

            await _unitOfWork.TransactionSuccessfulAsync(cancellationToken);
        }

        return response;
    }
}