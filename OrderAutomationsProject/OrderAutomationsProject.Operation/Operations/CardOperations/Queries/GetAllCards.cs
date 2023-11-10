using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.CardOperations.Queries;

public class GetAllCardsQueryHandler : IRequestHandler<GetAllCardQuery, ApiResponse<List<CardResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetAllCardsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CardResponse>>> Handle(GetAllCardQuery request, CancellationToken cancellationToken)
    {
        List<Card> list = unitOfWork.CardRepository.GetAll().Where(x => x.IsActive == true).ToList();

        List<CardResponse> mapped = mapper.Map<List<CardResponse>>(list);

        return new ApiResponse<List<CardResponse>>(mapped);
    }
}
