using MediatR;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Api.Controllers;

[Route("oa/api/v1/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private IMediator mediator;

    public AuthenticationController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost("login")]
    [EnableCors("AllowAllHeaders")]
    public async Task<ApiResponse<LoginResponse>> Post([FromBody] LoginRequest request)
    {
        var operation = new AuthenticationServiceCommand(request);
        var result = await mediator.Send(operation);
        return result;
    }
}
