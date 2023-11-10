using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.BillOperations.Commands;

public class DeleteBillCommandHandler : IRequestHandler<DeleteBillCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;

    public DeleteBillCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(DeleteBillCommand request, CancellationToken cancellationToken)
    {
        // Finding Bill to delete
        Bill entity = await unitOfWork.BillRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse("Record not found!");
        }

        // Changing entity state
        entity.IsActive = false;

        // Saving changes to the database
        unitOfWork.CompleteAsync(cancellationToken);

        // Returning response as ApiResponse type
        return new ApiResponse();
    }
}
