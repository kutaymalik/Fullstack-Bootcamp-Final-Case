using MediatR;
using Microsoft.AspNetCore.Mvc;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Api.Controllers;

[Route("oa/api/v1/[controller]")]
[ApiController]
public class TokenController : ControllerBase
{
    private IMediator mediator;

    public TokenController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    public async Task<ApiResponse<TokenResponse>> CreateToken([FromBody] TokenRequest request)
    {
        var operation = new CreateTokenCommand(request);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpGet("refreshToken")]
    public async Task<ApiResponse<TokenResponse>> RefreshToken([FromQuery] string token)
    {
        var operation = new RefreshTokenCommand(token);

        var result = await mediator.Send(operation);

        return result;
    }
}
