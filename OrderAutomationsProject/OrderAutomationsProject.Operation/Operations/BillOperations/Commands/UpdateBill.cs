//using MediatR;
//using OrderAutomationsProject.Base.Response;
//using OrderAutomationsProject.Data.Domain;
//using OrderAutomationsProject.Data.UnitOfWorks;
//using OrderAutomationsProject.Operation.Cqrs;

//namespace OrderAutomationsProject.Operation.Operations.BillOperations.Commands;

//public class UpdateBillCommandHandler : IRequestHandler<UpdateBillCommand, ApiResponse>
//{
//    private readonly IUnitOfWork unitOfWork;

//    public UpdateBillCommandHandler(IUnitOfWork unitOfWork)
//    {
//        this.unitOfWork = unitOfWork;
//    }

//    public async Task<ApiResponse> Handle(UpdateBillCommand request, CancellationToken cancellationToken)
//    {
//        // Finding required Bill to Bill Table
//        Bill entity = await unitOfWork.BillRepository.GetByIdAsync(request.Id, cancellationToken);

//        if (entity == null)
//        {
//            return new ApiResponse("Record not found!");
//        }

//        // Changing required fields
//        entity.BillDate = request.Model.BillDate != DateTime.MinValue ? request.Model.BillDate : entity.BillDate;
//        entity.DealerName = string.IsNullOrEmpty(request.Model.DealerName.Trim()) ? entity.DealerName : request.Model.DealerName;
//        entity.DealerAddress = string.IsNullOrEmpty(request.Model.DealerAddress.Trim()) ? entity.DealerAddress : request.Model.DealerAddress;
//        entity.DealerEmail = string.IsNullOrEmpty(request.Model.DealerEmail.Trim()) ? entity.DealerEmail : request.Model.DealerEmail;
//        entity.DealerPhone = string.IsNullOrEmpty(request.Model.DealerPhone.Trim()) ? entity.DealerPhone : request.Model.DealerPhone;

//        unitOfWork.BillRepository.Update(entity);
//        unitOfWork.CompleteAsync(cancellationToken);

//        // Returning response as ApiResponse type
//        return new ApiResponse();
//    }
//}
