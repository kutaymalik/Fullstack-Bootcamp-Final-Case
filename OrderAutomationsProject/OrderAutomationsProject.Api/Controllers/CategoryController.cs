using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Api.Controllers;

[Route("oa/api/v1/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator mediator;

    public CategoryController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [Authorize(Roles = "admin, dealer")]
    public async Task<ApiResponse<List<CategoryResponse>>> GetAll()
    {
        var operation = new GetAllCategoryQuery();

        var result = await mediator.Send(operation);

        return result;
    }


    [HttpGet("{id}")]
    [Authorize(Roles = "admin, dealer")]
    public async Task<ApiResponse<CategoryResponse>> GetById(int id)
    {
        var operation = new GetCategoryByIdQuery(id);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpPost]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse<CategoryResponse>> Post([FromBody] CategoryRequest request)
    {
        var operation = new CreateCategoryCommand(request);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Put(int id, [FromBody] CategoryRequest request)
    {
        var operation = new UpdateCategoryCommand(request, id);

        var result = await mediator.Send(operation);

        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteCategoryCommand(id);

        var result = await mediator.Send(operation);

        return result;
    }
}
