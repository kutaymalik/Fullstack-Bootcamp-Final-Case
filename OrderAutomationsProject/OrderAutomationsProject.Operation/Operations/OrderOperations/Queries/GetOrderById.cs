using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.OrderOperations.Queries;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, ApiResponse<OrderResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public GetOrderByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<OrderResponse>> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        Order order = new Order();
        if (sessionService.CheckSession().sessionRole == "admin")
        {
            order = unitOfWork.OrderRepository.FirstOrDefault(x => x.Id == request.Id && x.IsActive == true, "OrderItems.Product");
        }
        if (sessionService.CheckSession().sessionRole == "dealer")
        {
            int sessionId = sessionService.CheckSession().sessionId;
            order = unitOfWork.OrderRepository.FirstOrDefault(x => x.Id == request.Id && x.DealerId == sessionId && x.IsActive == true, "OrderItems.Product");
        }
        

        if (order == null)
        {
            return new ApiResponse<OrderResponse>("Records not found!");
        }

        OrderResponse mapped = mapper.Map<OrderResponse>(order);

        return new ApiResponse<OrderResponse>(mapped);
    }
}
