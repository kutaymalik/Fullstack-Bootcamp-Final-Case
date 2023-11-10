using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Api.Controllers;

[Route("oa/api/v1/[controller]")]
[ApiController]
public class HealthCheckController : ControllerBase
{
    [HttpGet]
    [Authorize(Roles = "dealer, admin")]
    public IActionResult HealthCheckWithToken()
    {
        return Ok("success");
    }

    [HttpGet("dontneedtoken")]
    public IActionResult HealthCheck()
    {
        return Ok("succes");
    }
}
