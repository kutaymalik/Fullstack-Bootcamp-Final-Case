using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Operation.Operations.AddressOperations.Queries;
using OrderAutomationsProject.Operation.Validation;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Api.Controllers;

[Route("oa/api/v1/[controller]")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IMediator mediator;

    public AddressController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<AddressResponse>>> GetAll()
    {
        var operation = new GetAllAddressQuery();

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpGet("{id}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<AddressResponse>> GetById(int id)
    {
        var operation = new GetAddressByIdQuery(id);

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpGet("ByDealerId")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<List<AddressResponse>>> GetByDealerId(int dealerid)
    {
        var operation = new GetAddressByDealerIdQuery(dealerid);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpPost]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<AddressResponse>> Post([FromBody] AddressRequest request)
    {
        var operation = new CreateAddressCommand(request);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse> Put(int id, [FromBody] AddressRequest request)
    {
        var operation = new UpdateAddressCommand(request, id);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteAddressCommand(id);

        var result = await mediator.Send(operation);

        return result;
    }
}
