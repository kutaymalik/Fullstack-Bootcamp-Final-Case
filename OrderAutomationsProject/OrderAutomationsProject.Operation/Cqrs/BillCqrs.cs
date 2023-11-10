using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Cqrs;

public record CreateBillCommand(BillRequest Model) : IRequest<ApiResponse<BillResponse>>;
public record UpdateBillCommand(BillRequest Model, int Id) : IRequest<ApiResponse>;
public record DeleteBillCommand(int Id) : IRequest<ApiResponse>;


public record GetAllBillQuery() : IRequest<ApiResponse<List<BillResponse>>>;
public record GetBillByBillNumberQuery(int BillNumber) : IRequest<ApiResponse<BillResponse>>;
public record GetBillByDealerIdQuery(int DealerId) : IRequest<ApiResponse<List<BillResponse>>>;