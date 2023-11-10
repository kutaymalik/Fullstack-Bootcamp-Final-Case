using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Cqrs;

public record CreateDealerCommand(DealerRequest Model) : IRequest<ApiResponse<DealerResponse>>;
public record UpdateDealerCommand(DealerRequest Model, int Id) : IRequest<ApiResponse>;
public record DeleteDealerCommand(int Id) : IRequest<ApiResponse>;


public record GetAllDealerQuery() : IRequest<ApiResponse<List<DealerResponse>>>;
public record GetDealerByIdQuery(int Id) : IRequest<ApiResponse<DealerResponse>>;
public record GetDealerBySession() : IRequest<ApiResponse<DealerResponse>>;
