using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.ProductOperations.Commands;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;

    public DeleteProductCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        // Finding Product to delete
        Product entity = await unitOfWork.ProductRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null || entity.IsActive == false)
        {
            var res = new ApiResponse("Record not found!");
            res.Success = false;
            return res;
        }

        // Changing entity state
        entity.IsActive = false;

        // Saving changes to the database
        await unitOfWork.CompleteAsync(cancellationToken);

        // Returning response as ApiResponse type
        return new ApiResponse();
    }
}
