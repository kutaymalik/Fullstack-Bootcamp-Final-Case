using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.PaymentOperations.Queries;

public class GetAllPaymentsQueryHandler : IRequestHandler<GetAllPaymentQuery, ApiResponse<List<PaymentResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetAllPaymentsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetAllPaymentQuery request, CancellationToken cancellationToken)
    {
        List<Payment> list = unitOfWork.PaymentRepository.Where(x => x.IsActive == true).ToList();

        List<PaymentResponse> mapped = mapper.Map<List<PaymentResponse>>(list);

        return new ApiResponse<List<PaymentResponse>>(mapped);
    }
}

