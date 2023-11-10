using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Operation.Validation;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.CategoryOperations.Commands;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, ApiResponse<CategoryResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        CategoryValidator validator = new CategoryValidator();
        var validationResult = validator.Validate(request.Model);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            var res = new ApiResponse<CategoryResponse>(errors);
            res.StatusCode = 400;
            res.Success = false;
            return res;
        }

        Category mapped = mapper.Map<Category>(request.Model);

        await unitOfWork.CategoryRepository.InsertAsync(mapped, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        var response = mapper.Map<CategoryResponse>(mapped);

        return new ApiResponse<CategoryResponse>(response);
    }
}
