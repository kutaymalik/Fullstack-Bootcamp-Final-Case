using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderItemAutomationsProject.Operation.Cqrs;

public record CreateOrderItemCommand(OrderItemRequest Model) : IRequest<ApiResponse<OrderItemResponse>>;
public record UpdateOrderItemCommand(OrderItemRequest Model, int Id) : IRequest<ApiResponse>;
public record DeleteOrderItemCommand(int Id) : IRequest<ApiResponse>;


public record GetAllOrderItemQuery() : IRequest<ApiResponse<List<OrderItemResponse>>>;
public record GetOrderItemByOrderNumberQuery(int OrderItemNumber) : IRequest<ApiResponse<OrderItemResponse>>;
