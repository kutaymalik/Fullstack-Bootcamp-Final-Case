//using MediatR;
//using OrderAutomationsProject.Base.Response;
//using OrderAutomationsProject.Data.Domain;
//using OrderAutomationsProject.Data.UnitOfWorks;
//using OrderAutomationsProject.Operation.Cqrs;

//namespace OrderAutomationsProject.Operation.Operations.OrderOperations.Commands;

//public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, ApiResponse>
//{
//    private readonly IUnitOfWork unitOfWork;

//    public UpdateOrderCommandHandler(IUnitOfWork unitOfWork)
//    {
//        this.unitOfWork = unitOfWork;
//    }

//    public async Task<ApiResponse> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
//    {
//        // Finding required Order to Order Table
//        Order entity = await unitOfWork.OrderRepository.GetByIdAsync(request.Id, cancellationToken);

//        if (entity == null)
//        {
//            return new ApiResponse("Record not found!");
//        }

//        // Changing required fields
//        entity.OrderNumber = request.Model.OrderNumber != 0 ? request.Model.OrderNumber : entity.OrderNumber;
//        entity.Confirmation = request.Model.Confirmation != default ? request.Model.Confirmation : entity.Confirmation;
//        entity.OrderDate = request.Model.OrderDate != DateTime.MinValue ? request.Model.OrderDate : entity.OrderDate;
//        entity.DealerName = string.IsNullOrEmpty(request.Model.DealerName.Trim()) ? entity.DealerName : request.Model.DealerName;
//        entity.DealerAddress = string.IsNullOrEmpty(request.Model.DealerAddress.Trim()) ? entity.DealerAddress : request.Model.DealerAddress;
//        entity.DealerEmail = string.IsNullOrEmpty(request.Model.DealerEmail.Trim()) ? entity.DealerEmail : request.Model.DealerEmail;
//        entity.DealerPhone = string.IsNullOrEmpty(request.Model.DealerPhone.Trim()) ? entity.DealerPhone : request.Model.DealerPhone;

//        unitOfWork.OrderRepository.Update(entity);
//        unitOfWork.CompleteAsync(cancellationToken);

//        // Returning response as ApiResponse type
//        return new ApiResponse();
//    }
//}
