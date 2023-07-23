using CleanArchitecture.Application.Contracts.Common.Pagination;
using CleanArchitecture.Application.Contracts.Customers.Requests;
using CleanArchitecture.Application.Contracts.Customers.Responses;
using CleanArchitecture.Application.Core.Customers.Commands.CreateCustomer;
using CleanArchitecture.Application.Core.Customers.Commands.DeleteCustomer;
using CleanArchitecture.Application.Core.Customers.Commands.UpdateCustomer;
using CleanArchitecture.Application.Core.Customers.Queries.GetCustomerById;
using CleanArchitecture.Application.Core.Customers.Queries.GetCustomers;
using CleanArchitecture.Web.Extensions;
using CleanArchitecture.Web.Infrastructure.Controllers;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers.V1;

public sealed class CustomersController : ApiV1Controller
{
    public CustomersController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<CustomerResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(
        string? email,
        string? name,
        string? address,
        int page,
        int pageSize,
        string? orderBy)
    {
        var query = new GetCustomersQuery(
            email,
            name,
            address,
            page,
            pageSize,
            orderBy
        );

        return await Result.Ok(query)
            .Bind(q => Sender.Send(q))
            .Match(Ok, BadRequest);
    }

    [HttpGet("{customerId:guid}")]
    [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid customerId)
    {
        var query = new GetCustomerByIdQuery(new(customerId));
    
        return await Result.Ok(query)
            .Bind(q => Sender.Send(q))
            .Match(Ok, NotFound);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateCustomerRequest request)
    {
        var command = new CreateCustomerCommand
        {
            Email = request.Email,
            Name = request.Name,
            Address = request.Address
        };

        return await Result.Ok(command)
            .Bind(c => Sender.Send(c))
            .Match(Created, BadRequest);
    }
    
    [HttpPut("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid customerId, UpdateCustomerRequest request)
    {
        var command = new UpdateCustomerCommand
        {
            CustomerId = new(customerId),
            Email = request.Email,
            Name = request.Name,
            Address = request.Address
        };

        return await Result.Ok(command)
            .Bind(c => Sender.Send(c))
            .Match(NoContent, BadRequest, NotFound);
    }
    
    [HttpDelete("{customerId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid customerId)
    {
        var command = new DeleteCustomerCommand(new(customerId));
        
        return await Result.Ok(command)
            .Bind(c => Sender.Send(c))
            .Match(NoContent, NotFound);
    }
}