using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Repositories.Customers;
using CleanArchitecture.Domain.Repositories.Orders;
using FluentResults;

namespace CleanArchitecture.Application.Core.Orders.Commands.UpdateOrder;

internal sealed class UpdateOrderCommandHandler : ICommandHandler<UpdateOrderCommand, Result>
{
    private readonly IOrderReadRepository _orderReadRepository;
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly ICustomerReadRepository _customerReadRepository;

    public UpdateOrderCommandHandler(
        IOrderReadRepository orderReadRepository,
        IOrderWriteRepository orderWriteRepository,
        ICustomerReadRepository customerReadRepository)
    {
        _orderReadRepository = orderReadRepository;
        _orderWriteRepository = orderWriteRepository;
        _customerReadRepository = customerReadRepository;
    }

    public async Task<Result> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderReadRepository.GetByIdAsync(request.OrderId, cancellationToken);
        if (order is null)
            return Result.Fail(Exceptions.ConstantErrors.Orders.NotFoundById.WithMetadata("Id", request.OrderId));
        
        var customer = await _customerReadRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
            return Result.Fail(Exceptions.ConstantErrors.Customers.NotFoundById.WithMetadata("Id", request.CustomerId));
        
        order.Update(
            customer.Id,
            request.Comment);

        _orderWriteRepository.Update(order);
        
        return Result.Ok();
    }
}