using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Cqrs;

public record CreateCategoryCommand(CategoryRequest Model) : IRequest<ApiResponse<CategoryResponse>>;
public record UpdateCategoryCommand(CategoryRequest Model, int Id) : IRequest<ApiResponse>;
public record DeleteCategoryCommand(int Id) : IRequest<ApiResponse>;


public record GetAllCategoryQuery() : IRequest<ApiResponse<List<CategoryResponse>>>;
public record GetCategoryByIdQuery(int Id) : IRequest<ApiResponse<CategoryResponse>>;
