using CleanArchitecture.Application.Contracts.Common.Pagination;
using CleanArchitecture.Application.Contracts.Orders.Requests;
using CleanArchitecture.Application.Contracts.Orders.Responses;
using CleanArchitecture.Application.Core.Orders.Commands.CreateOrder;
using CleanArchitecture.Application.Core.Orders.Commands.DeleteOrder;
using CleanArchitecture.Application.Core.Orders.Commands.UpdateOrder;
using CleanArchitecture.Application.Core.Orders.Queries.GetOrderById;
using CleanArchitecture.Application.Core.Orders.Queries.GetOrders;
using CleanArchitecture.Web.Extensions;
using CleanArchitecture.Web.Infrastructure.Controllers;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers.V1;

public sealed class OrdersController : ApiV1Controller
{
    public OrdersController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<OrderResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(
        Guid? customerId,
        string? comment,
        int page,
        int pageSize,
        string? orderBy)
    {
        var query = new GetOrdersQuery(
            customerId,
            comment,
            page,
            pageSize,
            orderBy
        );

        return await Result.Ok(query)
            .Bind(q => Sender.Send(q))
            .Match(Ok, BadRequest);
    }

    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(typeof(OrderResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid orderId)
    {
        var query = new GetOrderByIdQuery(new(orderId));
    
        return await Result.Ok(query)
            .Bind(q => Sender.Send(q))
            .Match(Ok, NotFound);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateOrderRequest request)
    {
        var command = new CreateOrderCommand
        {
            CustomerId = new(request.CustomerId),
            Comment = request.Comment
        };

        return await Result.Ok(command)
            .Bind(c => Sender.Send(c))
            .Match(Created, BadRequest);
    }
    
    [HttpPut("{orderId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid orderId, UpdateOrderRequest request)
    {
        var command = new UpdateOrderCommand
        {
            OrderId = new(orderId),
            CustomerId = new(request.CustomerId),
            Comment = request.Comment
        };
        
        return await Result.Ok(command)
            .Bind(c => Sender.Send(c))
            .Match(NoContent, BadRequest, NotFound);
    }
    
    [HttpDelete("{orderId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid orderId)
    {
        var command = new DeleteOrderCommand(new(orderId));
        
        return await Result.Ok(command)
            .Bind(c => Sender.Send(c))
            .Match(NoContent, NotFound);
    }
}