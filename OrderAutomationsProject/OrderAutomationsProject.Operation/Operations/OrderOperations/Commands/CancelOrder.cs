using MediatR;
using OrderAutomationsProject.Base.PaymentType;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.OrderOperations.Commands;

public class CancelOrderCommandHandler : IRequestHandler<CancelOrderCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;

    public CancelOrderCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(CancelOrderCommand request, CancellationToken cancellationToken)
    {
        Order entity = await unitOfWork.OrderRepository.GetByIdAsync(request.Id, cancellationToken, "OrderItems","Dealer");

        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        if(entity.Confirmation == true)
        {
            return new ApiResponse("Confirmed order can not cancel!");
        }

        entity.Confirmation = false;
        unitOfWork.OrderRepository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        if (entity.OpenAccountOrder == true)
        {
            entity.Dealer.OpenAccountLimit += entity.TotalAmount;
        }

        // Returning response as ApiResponse type
        return new ApiResponse("Order cancelled.");
    }
}
