using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.CardOperations.Queries;

public class GetCardByIdQueryHandler : IRequestHandler<GetCardByIdQuery, ApiResponse<CardResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public GetCardByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<CardResponse>> Handle(GetCardByIdQuery request, CancellationToken cancellationToken)
    {
        Card entity = new Card();
        if (sessionService.CheckSession().sessionRole == "admin")
        {
            entity = unitOfWork.CardRepository.FirstOrDefault(x => x.Id == request.Id && x.IsActive == true, "OrderItems");
        }
        if (sessionService.CheckSession().sessionRole == "dealer")
        {
            int sessionId = sessionService.CheckSession().sessionId;
            entity = unitOfWork.CardRepository.FirstOrDefault(x => x.Id == request.Id && x.DealerId == sessionId && x.IsActive == true, "Dealer");
        }

        if (entity == null)
        {
            return new ApiResponse<CardResponse>("Record not found!");
        }

        CardResponse mapped = mapper.Map<CardResponse>(entity);

        return new ApiResponse<CardResponse>(mapped);
    }
}

