using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.CostOperations.Queries;

public class GetHistoricalCostsQueryHandler : IRequestHandler<GetHistoricalCostsQuery, ApiResponse<List<CostResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public GetHistoricalCostsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<List<CostResponse>>> Handle(GetHistoricalCostsQuery request, CancellationToken cancellationToken)
    {
        int sessionId = sessionService.CheckSession().sessionId;
        List<Cost> list = unitOfWork.CostRepository.Where(x => x.DealerId == sessionId && x.Confirmation != null && x.IsActive == true, "Dealer").ToList();

        List<CostResponse> mapped = mapper.Map<List<CostResponse>>(list);

        return new ApiResponse<List<CostResponse>>(mapped);
    }
}
