using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.OrderOperations.Queries;

public class GetOrderByDealerId : IRequestHandler<GetOrderByDealerIdQuery, ApiResponse<List<OrderResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public GetOrderByDealerId(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<List<OrderResponse>>> Handle(GetOrderByDealerIdQuery request, CancellationToken cancellationToken)
    {
        List<Order> list = new List<Order>();
        if (sessionService.CheckSession().sessionRole == "admin")
        {
            list = unitOfWork.OrderRepository.Where(x => x.DealerId == request.DealerId && x.IsActive == true, "OrderItems").ToList();
        }
        if (sessionService.CheckSession().sessionRole == "dealer")
        {
            int sessionId = sessionService.CheckSession().sessionId;
            list = unitOfWork.OrderRepository.Where(x => x.DealerId == sessionId && x.IsActive == true, "OrderItems").ToList();
        }

        List<OrderResponse> mapped = mapper.Map<List<OrderResponse>>(list);

        return new ApiResponse<List<OrderResponse>>(mapped);
    }
}
