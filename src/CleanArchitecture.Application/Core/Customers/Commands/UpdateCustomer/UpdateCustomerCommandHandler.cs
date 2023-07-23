using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Repositories.Customers;
using FluentResults;

namespace CleanArchitecture.Application.Core.Customers.Commands.UpdateCustomer;

internal sealed class UpdateCustomerCommandHandler : ICommandHandler<UpdateCustomerCommand, Result>
{
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly ICustomerWriteRepository _customerWriteRepository;

    public UpdateCustomerCommandHandler(
        ICustomerReadRepository customerReadRepository,
        ICustomerWriteRepository customerWriteRepository)
    {
        _customerReadRepository = customerReadRepository;
        _customerWriteRepository = customerWriteRepository;
    }

    public async Task<Result> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerReadRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
            return Result.Fail(Exceptions.ConstantErrors.Customers.NotFoundById.WithMetadata("Id", request.CustomerId));
        
        customer.Update(
            request.Email,
            request.Name,
            request.Address);

        _customerWriteRepository.Update(customer);
        
        return Result.Ok();
    }
}