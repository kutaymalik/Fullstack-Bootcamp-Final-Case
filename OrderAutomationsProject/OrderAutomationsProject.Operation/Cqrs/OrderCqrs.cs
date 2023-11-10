using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Cqrs;

public record CreateOrderCommand(OrderRequest Model) : IRequest<ApiResponse<OrderResponse>>;
public record UpdateOrderCommand(OrderRequest Model, int Id) : IRequest<ApiResponse>;
public record DeleteOrderCommand(int Id) : IRequest<ApiResponse>;
public record ConfirmOrderCommand(int Id) : IRequest<ApiResponse>;
public record CancelOrderCommand(int Id) : IRequest<ApiResponse>;


public record GetAllOrderQuery() : IRequest<ApiResponse<List<OrderResponse>>>;
public record GetOrderByIdQuery(int Id) : IRequest<ApiResponse<OrderResponse>>;
public record GetOrderByDealerIdQuery(int DealerId) : IRequest<ApiResponse<List<OrderResponse>>>;
