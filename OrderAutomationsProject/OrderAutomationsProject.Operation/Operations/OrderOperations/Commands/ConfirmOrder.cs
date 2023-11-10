using AutoMapper;
using Azure.Core;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.OrderOperations.Commands;

public class ConfirmOrderCommandHandler : IRequestHandler<ConfirmOrderCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;

    public ConfirmOrderCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(ConfirmOrderCommand request, CancellationToken cancellationToken)
    {
        Order entity = unitOfWork.OrderRepository.FirstOrDefault(x => x.Id == request.Id, "OrderItems");

        if (entity == null || entity.IsActive == false)
        {
            return new ApiResponse("Record not found!");
        }

        entity.OrderNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(1,6).ToLower();
        entity.Confirmation = true;

        unitOfWork.OrderRepository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        // Returning response as ApiResponse type
        return new ApiResponse("Order confirmed.");
    }
}
