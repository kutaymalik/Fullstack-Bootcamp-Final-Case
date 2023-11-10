using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Operation.Operations.CardOperations.Queries;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Api.Controllers;

[Route("oa/api/v1/[controller]")]
[ApiController]
public class CardController : ControllerBase
{
    private readonly IMediator mediator;

    public CardController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<CardResponse>>> GetAll()
    {
        var operation = new GetAllCardQuery();

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpGet("{id}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<CardResponse>> GetById(int id)
    {
        var operation = new GetCardByIdQuery(id);

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpGet("ByDealerId")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<List<CardResponse>>> GetByDaalerId(int dealerid)
    {
        var operation = new GetCardByDealerIdQuery(dealerid);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpPost]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<CardResponse>> Post([FromBody] CardRequest request)
    {
        var operation = new CreateCardCommand(request);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse> Put(int id, [FromBody] UpdateCardRequest request)
    {
        var operation = new UpdateCardCommand(request, id);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteCardCommand(id);

        var result = await mediator.Send(operation);

        return result;
    }
}
