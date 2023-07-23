using System.Net;
using CleanArchitecture.Web.Helpers;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Infrastructure.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]/")]
public abstract class ApiBaseController : ControllerBase
{
    protected readonly ISender Sender;

    protected ApiBaseController(ISender sender)
    {
        Sender = sender;
    }

    protected IActionResult BadRequest(IList<IError> errors) =>
        base.BadRequest(ApiErrorResponseBuilder.Build(HttpContext, HttpStatusCode.BadRequest, errors));

    protected IActionResult Ok<T>(T value) => base.Ok(value);

    protected IActionResult Created() => base.Created(string.Empty, null);
    
    protected IActionResult NotFound(IList<IError> errors) =>
        base.NotFound(ApiErrorResponseBuilder.Build(HttpContext, HttpStatusCode.NotFound, errors));
}