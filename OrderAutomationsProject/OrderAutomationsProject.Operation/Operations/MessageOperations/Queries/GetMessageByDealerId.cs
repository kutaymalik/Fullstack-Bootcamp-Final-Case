using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.MessageOperations.Queries;

public class GetMessagesByDealerIdQueryHandler : IRequestHandler<GetMessagesByDealerIdQuery, ApiResponse<List<MessageResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public GetMessagesByDealerIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<List<MessageResponse>>> Handle(GetMessagesByDealerIdQuery request, CancellationToken cancellationToken)
    {
        List<Message> list = new List<Message>();

        list = list = unitOfWork.MessageRepository.Where(x => x.ReceiverId == request.DealerId || x.SenderId == request.DealerId).OrderBy(x => x.SentAt).ToList();

        List<MessageResponse> mapped = mapper.Map<List<MessageResponse>>(list);

        return new ApiResponse<List<MessageResponse>>(mapped);
    }
}
