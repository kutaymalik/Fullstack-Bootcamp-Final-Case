using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.ProductOperations.Commands;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;

    public UpdateProductCommandHandler(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        // Finding required Product to Product Table
        Product entity = await unitOfWork.ProductRepository.GetByIdAsync(request.Id, cancellationToken, "Category");

        if (entity == null)
        {
            var res = new ApiResponse("Record not found!");
            res.Success = false;
            return res;
        }

        // Changing required fields
        entity.Price = (request.Model.Price != default && request.Model.Price != 0) ? request.Model.Price : entity.Price;
        entity.StockQuantity = (request.Model.StockQuantity != default && request.Model.StockQuantity != 0) ? request.Model.StockQuantity : entity.StockQuantity;
        entity.ProductName = (string.IsNullOrEmpty(request.Model.ProductName.Trim()) || request.Model.ProductName.ToLower() == "string") ? entity.ProductName : request.Model.ProductName;
        entity.ProductDescription = (string.IsNullOrEmpty(request.Model.ProductDescription.Trim()) || request.Model.ProductDescription.ToLower() == "string") ? entity.ProductDescription : request.Model.ProductDescription;
        entity.CategoryId = (request.Model.CategoryId != default && request.Model.CategoryId != 0) ? request.Model.CategoryId : entity.CategoryId;

        unitOfWork.ProductRepository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        // Returning response as ApiResponse type
        return new ApiResponse();
    }
}
