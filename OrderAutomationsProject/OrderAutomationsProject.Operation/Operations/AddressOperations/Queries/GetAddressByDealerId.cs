using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.AddressOperations.Queries;

public class GetAddressByDealerId : IRequestHandler<GetAddressByDealerIdQuery, ApiResponse<List<AddressResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public GetAddressByDealerId(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<List<AddressResponse>>> Handle(GetAddressByDealerIdQuery request, CancellationToken cancellationToken)
    {
        List<Address> list = new List<Address>();
        if (sessionService.CheckSession().sessionRole == "admin")
        {
            list = unitOfWork.AddressRepository.Where(x => x.DealerId == request.DealerId && x.IsActive == true, "OrderItems").ToList();
        }
        if (sessionService.CheckSession().sessionRole == "dealer")
        {
            int sessionId = sessionService.CheckSession().sessionId;
            list = unitOfWork.AddressRepository.Where(x => x.DealerId == sessionId && x.IsActive == true, "Dealer").ToList();
        }

        List<AddressResponse> mapped = mapper.Map<List<AddressResponse>>(list);

        return new ApiResponse<List<AddressResponse>>(mapped);
    }
}
