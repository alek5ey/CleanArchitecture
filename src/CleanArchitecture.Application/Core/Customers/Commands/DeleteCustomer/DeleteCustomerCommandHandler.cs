using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Repositories.Customers;
using FluentResults;

namespace CleanArchitecture.Application.Core.Customers.Commands.DeleteCustomer;

internal sealed class DeleteCustomerCommandHandler : ICommandHandler<DeleteCustomerCommand, Result>
{
    private readonly ICustomerReadRepository _customerReadRepository;
    private readonly ICustomerWriteRepository _customerWriteRepository;

    public DeleteCustomerCommandHandler(
        ICustomerReadRepository customerReadRepository,
        ICustomerWriteRepository customerWriteRepository)
    {
        _customerReadRepository = customerReadRepository;
        _customerWriteRepository = customerWriteRepository;
    }

    public async Task<Result> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _customerReadRepository.GetByIdAsync(request.CustomerId, cancellationToken);
        if (customer is null)
            return Result.Fail(Exceptions.ConstantErrors.Customers.NotFoundById.WithMetadata("Id", request.CustomerId));
        
        customer.Delete();
        
        _customerWriteRepository.Remove(customer);
        
        return Result.Ok();
    }
}