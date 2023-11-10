using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.CardOperations.Queries;

public class GetCardByDealerId : IRequestHandler<GetCardByDealerIdQuery, ApiResponse<List<CardResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public GetCardByDealerId(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<List<CardResponse>>> Handle(GetCardByDealerIdQuery request, CancellationToken cancellationToken)
    {
        List<Card> list = new List<Card>();
        if (sessionService.CheckSession().sessionRole == "admin")
        {
            list = unitOfWork.CardRepository.Where(x => x.DealerId == request.DealerId && x.IsActive == true, "Dealer").ToList();
        }
        if (sessionService.CheckSession().sessionRole == "dealer")
        {
            int sessionId = sessionService.CheckSession().sessionId;
            list = unitOfWork.CardRepository.Where(x => x.DealerId == sessionId && x.IsActive == true, "Dealer").ToList();
        }

        List<CardResponse> mapped = mapper.Map<List<CardResponse>>(list);

        return new ApiResponse<List<CardResponse>>(mapped);
    }
}
