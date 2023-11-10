using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace PaymentAutomationsProject.Api.Controllers;

[Route("oa/api/v1/[controller]")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly IMediator mediator;

    public PaymentController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "dealer")]
    public async Task<ApiResponse<PaymentResponse>> Post([FromBody] PaymentRequest request)
    {
        var operation = new CreatePaymentCommand(request);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpGet]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<PaymentResponse>>> GetAll()
    {
        var operation = new GetAllPaymentQuery();

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpGet("ByDealerId")]
    [Authorize(Roles = "dealer, admin")]
    public async Task<ApiResponse<List<PaymentResponse>>> GetByDealerId(int dealerid)
    {
        var operation = new GetPaymentByDealerIdQuery(dealerid);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpGet("ByOrderId/{orderId}")]
    [Authorize(Roles = "dealer, admin")]
    public async Task<ApiResponse<PaymentResponse>> GetByOrderNumber(int orderId)
    {
        var operation = new GetPaymentByOrderIdQuery(orderId);

        var result = await mediator.Send(operation);

        return result;
    }
}
