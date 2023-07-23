using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.Orders;
using CleanArchitecture.Domain.Repositories.Customers;
using CleanArchitecture.Domain.Repositories.Orders;
using FluentResults;

namespace CleanArchitecture.Application.Core.Orders.Commands.CreateOrder;

internal sealed class CreateOrderCommandHandler : ICommandHandler<CreateOrderCommand, Result>
{
    private readonly IOrderWriteRepository _orderWriteRepository;
    private readonly ICustomerReadRepository _customerReadRepository;

    public CreateOrderCommandHandler(
        IOrderWriteRepository orderWriteRepository,
        ICustomerReadRepository customerReadRepository)
    {
        _orderWriteRepository = orderWriteRepository;
        _customerReadRepository = customerReadRepository;
    }

    public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerReadRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
            return Result.Fail(Exceptions.ConstantErrors.Customers.NotFoundById.WithMetadata("Id", request.CustomerId));
        
        var order = Order.Create(
            new(Guid.NewGuid()),
            customer.Id,
            DateTimeOffset.Now,
            request.Comment);
        
        await _orderWriteRepository.AddAsync(order, cancellationToken);
        
        return Result.Ok();
    }
}