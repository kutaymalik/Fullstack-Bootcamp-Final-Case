using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.BillOperations.Queries;

public class GetBillByBillNumberQueryHandler : IRequestHandler<GetBillByBillNumberQuery, ApiResponse<BillResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetBillByBillNumberQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<BillResponse>> Handle(GetBillByBillNumberQuery request, CancellationToken cancellationToken)
    {
        Bill entity = await unitOfWork.BillRepository.GetByIdAsync(request.BillNumber, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<BillResponse>("Record not found!");
        }

        BillResponse mapped = mapper.Map<BillResponse>(entity);

        return new ApiResponse<BillResponse>(mapped);
    }
}
