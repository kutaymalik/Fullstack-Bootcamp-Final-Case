using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Api.Controllers;

[Route("oa/api/v1/[controller]")]
[ApiController]
public class DealerController
{ 

    private readonly IMediator mediator;

    public DealerController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<DealerResponse>>> GetAll()
    {
        var operation = new GetAllDealerQuery();

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpGet("{id}")]
    [Authorize(Roles = "admin, dealer")]
    public async Task<ApiResponse<DealerResponse>> GetById(int id)
    {
        var operation = new GetDealerByIdQuery(id);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpGet("bysessionid")]
    [Authorize(Roles = "admin, dealer")]
    public async Task<ApiResponse<DealerResponse>> GetBySessionId()
    {
        var operation = new GetDealerBySession();

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpPost]
    public async Task<ApiResponse<DealerResponse>> Post([FromBody] DealerRequest request)
    {
        var operation = new CreateDealerCommand(request);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "dealer, admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] DealerRequest request)
    {
        var operation = new UpdateDealerCommand(request, id);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteDealerCommand(id);

        var result = await mediator.Send(operation);

        return result;
    }
}
