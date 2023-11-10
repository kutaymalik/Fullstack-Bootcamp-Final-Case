using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Api.Controllers;

[Route("oa/api/v1/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IMediator mediator;

    public ReportController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<List<int>>> GetAllOrdersReport([FromBody] ReportRequest request)
    {
        var operation = new CreateReportCommand(request);

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpPost("ByDealerId")]
    [Authorize(Roles = "dealer, admin")]
    public async Task<ApiResponse<List<int>>> GetDealerBasedReport([FromBody] DealerBasedReportRequest request)
    {
        var operation = new CreateDealerReportCommand(request);

        var result = await mediator.Send(operation);

        return result;
    }
}
