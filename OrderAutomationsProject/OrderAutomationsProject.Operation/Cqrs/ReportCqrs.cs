using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Cqrs;

public record CreateReportCommand(ReportRequest Model) : IRequest<ApiResponse<List<int>>>;
public record CreateDealerReportCommand(DealerBasedReportRequest Model) : IRequest<ApiResponse<List<int>>>;