using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Cqrs;

public record CreatePaymentCommand(PaymentRequest Model) : IRequest<ApiResponse<PaymentResponse>>;
public record UpdatePaymentCommand(PaymentRequest Model, int Id) : IRequest<ApiResponse>;
public record DeletePaymentCommand(int Id) : IRequest<ApiResponse>;


public record GetAllPaymentQuery() : IRequest<ApiResponse<List<PaymentResponse>>>;
public record GetPaymentByOrderIdQuery(int OrderId) : IRequest<ApiResponse<PaymentResponse>>;
public record GetPaymentByDealerIdQuery(int DealerId) : IRequest<ApiResponse<List<PaymentResponse>>>;