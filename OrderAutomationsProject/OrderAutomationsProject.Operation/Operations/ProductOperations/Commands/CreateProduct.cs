using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Operation.Validation;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.ProductOperations.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ApiResponse<ProductResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ProductResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        ProductValidator validator = new ProductValidator();
        var validationResult = validator.Validate(request.Model);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            var res = new ApiResponse<ProductResponse>(errors);
            res.StatusCode = 400;
            res.Success = false;
            return res;
        }

        Product mapped = mapper.Map<Product>(request.Model);

        await unitOfWork.ProductRepository.InsertAsync(mapped, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        var response = mapper.Map<ProductResponse>(mapped);

        return new ApiResponse<ProductResponse>(response);
    }
}
