using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Cqrs;

public record CreateCostCommand(CostRequest Model) : IRequest<ApiResponse<CostResponse>>;
public record UpdateCostCommand(CostRequest Model, int Id) : IRequest<ApiResponse>;
public record DeleteCostCommand(int Id) : IRequest<ApiResponse>;
public record ConfirmCostCommand(int Id) : IRequest<ApiResponse>;


public record GetHistoricalCostsQuery(int DealerId) : IRequest<ApiResponse<List<CostResponse>>>;

public record GetActiveCostsQuery(int DealerId) : IRequest<ApiResponse<List<CostResponse>>>;
