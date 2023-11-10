using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Cqrs;

public record CreateProductCommand(ProductRequest Model) : IRequest<ApiResponse<ProductResponse>>;
public record UpdateProductCommand(ProductRequest Model, int Id) : IRequest<ApiResponse>;
public record DeleteProductCommand(int Id) : IRequest<ApiResponse>;


public record GetAllProductQuery() : IRequest<ApiResponse<List<ProductResponse>>>;
public record GetProductByIdQuery(int Id) : IRequest<ApiResponse<ProductResponse>>;
public record GetProductByCategoryIdQuery(int Id) : IRequest<ApiResponse<List<ProductResponse>>>;
