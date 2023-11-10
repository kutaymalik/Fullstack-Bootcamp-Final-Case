using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.CategoryOperations.Queries;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, ApiResponse<CategoryResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<CategoryResponse>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        Category entity = await unitOfWork.CategoryRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null || entity.IsActive == false)
        {
            return new ApiResponse<CategoryResponse>("Record not found!");
        }

        CategoryResponse mapped = mapper.Map<CategoryResponse>(entity);

        return new ApiResponse<CategoryResponse>(mapped);
    }
}
