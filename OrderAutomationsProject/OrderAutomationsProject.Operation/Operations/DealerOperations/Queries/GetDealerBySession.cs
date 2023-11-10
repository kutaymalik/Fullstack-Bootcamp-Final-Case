using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.DealerOperations.Queries;

public class GetDealerBySessionQueryHandler : IRequestHandler<GetDealerBySession, ApiResponse<DealerResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public GetDealerBySessionQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<DealerResponse>> Handle(GetDealerBySession request, CancellationToken cancellationToken)
    {
        var sessionid =  sessionService.CheckSession().sessionId;
        Dealer dealer = unitOfWork.DealerRepository.FirstOrDefault(x => x.Id == sessionid && x.IsActive == true);
        
        if(dealer == null)
        {
            throw new InvalidOperationException("Dealer not found!");
        }

        DealerResponse mapped = mapper.Map<DealerResponse>(dealer);

        return new ApiResponse<DealerResponse>(mapped);
    }
}
