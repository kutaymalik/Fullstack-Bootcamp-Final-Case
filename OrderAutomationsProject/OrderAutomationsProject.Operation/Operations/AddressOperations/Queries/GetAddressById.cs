using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.AddressOperations.Queries;

public class GetAddressByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, ApiResponse<AddressResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public GetAddressByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<AddressResponse>> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
    {
        Address entity = new Address();
        if (sessionService.CheckSession().sessionRole == "admin")
        {
            entity = unitOfWork.AddressRepository.FirstOrDefault(x => x.Id == request.Id && x.IsActive == true, "OrderItems");
        }
        if (sessionService.CheckSession().sessionRole == "dealer")
        {
            int sessionId = sessionService.CheckSession().sessionId;
            entity = unitOfWork.AddressRepository.FirstOrDefault(x => x.Id == request.Id && x.DealerId == sessionId && x.IsActive == true, "Dealer");
        }
        

        if (entity == null || entity.IsActive == false)
        {
            return new ApiResponse<AddressResponse>("Record not found!");
        }

        AddressResponse mapped = mapper.Map<AddressResponse>(entity);

        return new ApiResponse<AddressResponse>(mapped);
    }
}
