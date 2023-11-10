using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.PaymentOperations.Queries;

public class GetPaymentByOrderIdQueryHandler : IRequestHandler<GetPaymentByOrderIdQuery, ApiResponse<PaymentResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public GetPaymentByOrderIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<PaymentResponse>> Handle(GetPaymentByOrderIdQuery request, CancellationToken cancellationToken)
    {
        Payment entity = new Payment();
        if (sessionService.CheckSession().sessionRole == "admin")
        {
            entity = unitOfWork.PaymentRepository.FirstOrDefault(x => x.Order.Id == request.OrderId, "Card");
        }
        if (sessionService.CheckSession().sessionRole == "dealer")
        {
            int sessionId = sessionService.CheckSession().sessionId;
            entity = unitOfWork.PaymentRepository.FirstOrDefault(x => x.Order.Id == request.OrderId && x.Order.DealerId == sessionId, "Card");
        }

        if (entity == null)
        {
            return new ApiResponse<PaymentResponse>("Record not found!");
        }

        PaymentResponse mapped = mapper.Map<PaymentResponse>(entity);

        return new ApiResponse<PaymentResponse>(mapped);
    }
}
