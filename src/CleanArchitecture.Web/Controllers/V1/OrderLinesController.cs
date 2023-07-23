using CleanArchitecture.Application.Contracts.OrderLines.Requests;
using CleanArchitecture.Application.Contracts.Products.Responses;
using CleanArchitecture.Application.Core.OrderLines.Commands.CreateOrderLine;
using CleanArchitecture.Application.Core.OrderLines.Commands.DeleteOrderLine;
using CleanArchitecture.Application.Core.OrderLines.Commands.UpdateOrderLine;
using CleanArchitecture.Application.Core.OrderLines.Queries.GetOrderLines;
using CleanArchitecture.Web.Extensions;
using CleanArchitecture.Web.Infrastructure.Controllers;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers.V1;

public sealed class OrderLinesController : ApiV1Controller
{
    public OrderLinesController(ISender sender) : base(sender)
    {
    }

    [HttpGet("{orderId:guid}")]
    [ProducesResponseType(typeof(IReadOnlyList<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(Guid orderId)
    {
        var query = new GetOrderLinesQuery(new(orderId));

        return await Result.Ok(query)
            .Bind(q => Sender.Send(q))
            .Match(Ok, BadRequest);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateOrderLineRequest request)
    {
        var command = new CreateOrderLineCommand
        {
            OrderId = new(request.OrderId),
            ProductId = new(request.ProductId),
            Quantity = request.Quantity
        };

        return await Result.Ok(command)
            .Bind(c => Sender.Send(c))
            .Match(Created, BadRequest);
    }
    
    [HttpPut("{orderLineId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid orderLineId, UpdateOrderLineRequest request)
    {
        var command = new UpdateOrderLineCommand
        {
            OrderLineId = new(orderLineId),
            OrderId = new(request.OrderId),
            ProductId = new(request.ProductId),
            Quantity = request.Quantity
        };
        
        return await Result.Ok(command)
            .Bind(c => Sender.Send(c))
            .Match(NoContent, BadRequest, NotFound);
    }
    
    [HttpDelete("{orderLineId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid orderLineId)
    {
        var command = new DeleteOrderLineCommand(new(orderLineId));
        
        return await Result.Ok(command)
            .Bind(c => Sender.Send(c))
            .Match(NoContent, NotFound);
    }
}