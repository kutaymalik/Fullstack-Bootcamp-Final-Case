using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.DealerOperations.Queries;

public class GetDealerByIdQueryHandler : IRequestHandler<GetDealerByIdQuery, ApiResponse<DealerResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetDealerByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<DealerResponse>> Handle(GetDealerByIdQuery request, CancellationToken cancellationToken)
    {
        Dealer entity = await unitOfWork.DealerRepository.GetByIdAsync(request.Id, cancellationToken, "Addresses", "Orders");

        if (entity == null || entity.IsActive == false)
        {
            return new ApiResponse<DealerResponse>("Record not found!");
        }

        DealerResponse mapped = mapper.Map<DealerResponse>(entity);

        return new ApiResponse<DealerResponse>(mapped);
    }
}
