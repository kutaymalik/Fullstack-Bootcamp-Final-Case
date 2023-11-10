using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.PaymentOperations.Queries;

public class GetPaymentByDealerId : IRequestHandler<GetPaymentByDealerIdQuery, ApiResponse<List<PaymentResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public GetPaymentByDealerId(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetPaymentByDealerIdQuery request, CancellationToken cancellationToken)
    {
        List<Payment> list = new List<Payment>();
        if (sessionService.CheckSession().sessionRole == "admin")
        {
            list = list = unitOfWork.PaymentRepository.Where(x => x.Order.DealerId == request.DealerId && x.IsActive == true, "Order.Dealer").ToList();
        }
        if (sessionService.CheckSession().sessionRole == "dealer")
        {
            int sessionId = sessionService.CheckSession().sessionId;
            list = unitOfWork.PaymentRepository.Where(x => x.Order.DealerId == sessionId && x.IsActive == true, "Order.Dealer").ToList();
        }

        List<PaymentResponse> mapped = mapper.Map<List<PaymentResponse>>(list);

        return new ApiResponse<List<PaymentResponse>>(mapped);
    }
}
