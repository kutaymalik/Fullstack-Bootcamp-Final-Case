using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.DealerOperations.Queries;

public class GetAllDealersQueryHandler : IRequestHandler<GetAllDealerQuery, ApiResponse<List<DealerResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetAllDealersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<DealerResponse>>> Handle(GetAllDealerQuery request, CancellationToken cancellationToken)
    {
        List<Dealer> list = unitOfWork.DealerRepository.GetAll().Where(x => x.IsActive == true).ToList();

        List<DealerResponse> mapped = mapper.Map<List<DealerResponse>>(list);

        return new ApiResponse<List<DealerResponse>>(mapped);
    }
}
