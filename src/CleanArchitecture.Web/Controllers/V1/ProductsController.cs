using CleanArchitecture.Application.Contracts.Common.Pagination;
using CleanArchitecture.Application.Contracts.Products.Requests;
using CleanArchitecture.Application.Contracts.Products.Responses;
using CleanArchitecture.Application.Core.Products.Commands.CreateProduct;
using CleanArchitecture.Application.Core.Products.Commands.DeleteProduct;
using CleanArchitecture.Application.Core.Products.Commands.UpdateProduct;
using CleanArchitecture.Application.Core.Products.Queries.GetProductById;
using CleanArchitecture.Application.Core.Products.Queries.GetProducts;
using CleanArchitecture.Web.Extensions;
using CleanArchitecture.Web.Infrastructure.Controllers;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Controllers.V1;

public sealed class ProductsController : ApiV1Controller
{
    public ProductsController(ISender sender) : base(sender)
    {
    }

    [HttpGet]
    [ProducesResponseType(typeof(PagedResponse<ProductResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Get(
        string? name,
        string? sku,
        int? amount,
        int page,
        int pageSize,
        string? orderBy)
    {
        var query = new GetProductsQuery(
            name,
            sku,
            amount,
            page,
            pageSize,
            orderBy
        );

        return await Result.Ok(query)
            .Bind(q => Sender.Send(q))
            .Match(Ok, BadRequest);
    }

    [HttpGet("{productId:guid}")]
    [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetById(Guid productId)
    {
        var query = new GetProductByIdQuery(new(productId));
    
        return await Result.Ok(query)
            .Bind(q => Sender.Send(q))
            .Match(Ok, NotFound);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Create(CreateProductRequest request)
    {
        var command = new CreateProductCommand
        {
            Name = request.Name,
            Sku = request.Sku,
            Currency = request.Currency,
            Amount = request.Amount
        };

        return await Result.Ok(command)
            .Bind(c => Sender.Send(c))
            .Match(Created, BadRequest);
    }
    
    [HttpPut("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(Guid productId, UpdateProductRequest request)
    {
        var command = new UpdateProductCommand
        {
            ProductId = new(productId),
            Name = request.Name,
            Sku = request.Sku,
            Currency = request.Currency,
            Amount = request.Amount
        };
        
        return await Result.Ok(command)
            .Bind(c => Sender.Send(c))
            .Match(NoContent, BadRequest, NotFound);
    }
    
    [HttpDelete("{productId:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(Guid productId)
    {
        var command = new DeleteProductCommand(new(productId));
        
        return await Result.Ok(command)
            .Bind(c => Sender.Send(c))
            .Match(NoContent, NotFound);
    }
}