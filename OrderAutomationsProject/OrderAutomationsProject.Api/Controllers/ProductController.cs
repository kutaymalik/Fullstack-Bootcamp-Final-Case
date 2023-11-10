using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Api.Controllers;

[Route("oa/api/v1/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IMediator mediator;

    public ProductController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "admin, dealer")]
    public async Task<ApiResponse<List<ProductResponse>>> GetAll()
    {
        var operation = new GetAllProductQuery();

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpGet("{id}")]
    [Authorize(Roles = "admin, dealer")]
    public async Task<ApiResponse<ProductResponse>> GetById(int id)
    {
        var operation = new GetProductByIdQuery(id);

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpGet("ByDealerId")]
    [Authorize(Roles = "admin, dealer")]
    public async Task<ApiResponse<List<ProductResponse>>> GetByDealerId(int categoryid)
    {
        var operation = new GetProductByCategoryIdQuery(categoryid);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<ProductResponse>> Post([FromBody] ProductRequest request)
    {
        var operation = new CreateProductCommand(request);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] ProductRequest request)
    {
        var operation = new UpdateProductCommand(request, id);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteProductCommand(id);

        var result = await mediator.Send(operation);

        return result;
    }
}
