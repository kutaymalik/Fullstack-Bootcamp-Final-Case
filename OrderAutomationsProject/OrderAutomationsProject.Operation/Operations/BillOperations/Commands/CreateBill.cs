using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.BillOperations.Commands;

public class CreateBillCommandHandler : IRequestHandler<CreateBillCommand, ApiResponse<BillResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateBillCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public Task<ApiResponse<BillResponse>> Handle(CreateBillCommand request, CancellationToken cancellationToken)
    {
        Order order = unitOfWork.OrderRepository.GetById(request.Model.OrderId);

        if(order == null)
        {
            throw new InvalidOperationException("Order not found!");
        }

        Bill bill = new Bill()
        {
            BillDate = DateTime.UtcNow,
            OrderId = request.Model.OrderId,
            DealerName = order.Dealer.FirstName + " " + order.Dealer.LastName,
            DealerAddress = order.Dealer.Addresses[0].AddressLine1,
            DealerEmail = order.Dealer.Email,
            DealerPhone = order.Dealer.PhoneNumber,
            //OrderNumber = order.OrderNumber,
            OrderItems = order.OrderItems,
            TotalAmount = order.TotalAmount
        };

        unitOfWork.BillRepository.InsertAsync(bill, cancellationToken);
        unitOfWork.CompleteAsync(cancellationToken);

        var response = mapper.Map<BillResponse>(bill);

        return Task.FromResult(new ApiResponse<BillResponse>(response));
    }
}
