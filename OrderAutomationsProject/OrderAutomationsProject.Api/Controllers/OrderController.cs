using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Api.Controllers;

[Route("oa/api/v1/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IMediator mediator;

    public OrderController(IMediator mediator)
    {
        this.mediator = mediator;
    }


    [HttpPost]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<OrderResponse>> Post([FromBody] OrderRequest request)
    {
        var operation = new CreateOrderCommand(request);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<OrderResponse>>> GetAll()
    {
        var operation = new GetAllOrderQuery();

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpGet("ByDealerId")]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<List<OrderResponse>>> GetByDealerId(int dealerid)
    {
        var operation = new GetOrderByDealerIdQuery(dealerid);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "dealer, admin")]
    public async Task<ApiResponse<OrderResponse>> GetById(int id)
    {
        var operation = new GetOrderByIdQuery(id);

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpPut("Confirm/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> ConfirmOrder(int id)
    {
        var operation = new ConfirmOrderCommand(id);

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpPut("Cancel/{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> CancelOrder(int id)
    {
        var operation = new CancelOrderCommand(id);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "dealer, admin")]
    public async Task<ApiResponse> DeleteOrderC(int id)
    {
        var operation = new DeleteOrderCommand(id);

        var result = await mediator.Send(operation);

        return result;
    }
}
