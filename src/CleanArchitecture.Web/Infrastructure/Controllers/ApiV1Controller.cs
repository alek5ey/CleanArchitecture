using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Web.Infrastructure.Controllers;

[ApiVersion("1.0")]
public abstract class ApiV1Controller : ApiBaseController
{
    protected ApiV1Controller(ISender sender) : base(sender)
    {
    }
}