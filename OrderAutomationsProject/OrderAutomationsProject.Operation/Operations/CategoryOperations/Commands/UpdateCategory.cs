using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.CategoryOperations.Commands;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        // Finding required Category to Category Table
        Category entity = await unitOfWork.CategoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null)
        {
            var res = new ApiResponse("Record not found!");
            res.Success = false;
            return res;
        }

        // Changing required fields
        entity.Name = (string.IsNullOrEmpty(request.Model.Name.Trim()) || request.Model.Name.ToLower() == "string") ? entity.Name : request.Model.Name;
        entity.Description = (string.IsNullOrEmpty(request.Model.Description.Trim()) || request.Model.Description.ToLower() == "string") ? entity.Description : request.Model.Description;

        unitOfWork.CategoryRepository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        // Returning response as ApiResponse type
        return new ApiResponse();
    }
}
