using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Api.Controllers;

[Route("oa/api/v1/[controller]")]
[ApiController]
public class CostController : ControllerBase
{
    private readonly IMediator mediator;

    public CostController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpGet("active/{id}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<List<CostResponse>>> GetActive(int id)
    {
        var operation = new GetActiveCostsQuery(id);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpGet("historical/{id}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<List<CostResponse>>> GetHistorical(int id)
    {
        var operation = new GetHistoricalCostsQuery(id);

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpPost]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<CostResponse>> Post([FromBody] CostRequest request)
    {
        var operation = new CreateCostCommand(request);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse> Put(int id, [FromBody] CostRequest request)
    {
        var operation = new UpdateCostCommand(request, id);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteCostCommand(id);

        var result = await mediator.Send(operation);

        return result;
    }
}
