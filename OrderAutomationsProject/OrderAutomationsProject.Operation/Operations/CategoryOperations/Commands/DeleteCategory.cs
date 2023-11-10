using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.CategoryOperations.Commands;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        // Finding Category to delete
        Category entity = await unitOfWork.CategoryRepository.GetByIdAsync(request.Id, cancellationToken, "Products");

        if (entity == null || entity.IsActive == false)
        {
            var res = new ApiResponse("Record not found!");
            res.Success = false;
            return res;
        }

        if (entity.Products.Any())
        {
            var res = new ApiResponse("This category contains products!");
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
