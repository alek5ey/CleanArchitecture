using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Entites.Customers;
using CleanArchitecture.Domain.Repositories.Customers;
using FluentResults;

namespace CleanArchitecture.Application.Core.Customers.Commands.CreateCustomer;

internal sealed class CreateCustomerCommandHandler : ICommandHandler<CreateCustomerCommand, Result>
{
    private readonly ICustomerWriteRepository _customerWriteRepository;

    public CreateCustomerCommandHandler(ICustomerWriteRepository customerWriteRepository)
    {
        _customerWriteRepository = customerWriteRepository;
    }

    public async Task<Result> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = Customer.Create(
            new(Guid.NewGuid()),
            request.Email,
            request.Name,
            request.Address);
        
        await _customerWriteRepository.AddAsync(customer, cancellationToken);
        
        return Result.Ok();
    }
}