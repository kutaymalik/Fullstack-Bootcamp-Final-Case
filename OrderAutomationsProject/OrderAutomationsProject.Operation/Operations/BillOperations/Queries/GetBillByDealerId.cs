using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.BillOperations.Queries;

public class GetBillByDealerId : IRequestHandler<GetBillByDealerIdQuery, ApiResponse<List<BillResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetBillByDealerId(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<BillResponse>>> Handle(GetBillByDealerIdQuery request, CancellationToken cancellationToken)
    {
        List<Bill> list = unitOfWork.BillRepository.Where(x => x.Order.DealerId == request.DealerId, "Order.Dealer").ToList();

        List<BillResponse> mapped = mapper.Map<List<BillResponse>>(list);

        return new ApiResponse<List<BillResponse>>(mapped);
    }
}
