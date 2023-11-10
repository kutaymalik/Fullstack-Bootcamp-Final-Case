using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.BillOperations.Queries;

public class GetAllBillsQueryHandler : IRequestHandler<GetAllBillQuery, ApiResponse<List<BillResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetAllBillsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<BillResponse>>> Handle(GetAllBillQuery request, CancellationToken cancellationToken)
    {
        List<Bill> list = unitOfWork.BillRepository.GetAll();

        List<BillResponse> mapped = mapper.Map<List<BillResponse>>(list);
        
        return new ApiResponse<List<BillResponse>>(mapped);
    }
}
