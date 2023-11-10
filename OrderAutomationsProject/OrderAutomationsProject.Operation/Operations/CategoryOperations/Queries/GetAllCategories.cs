using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.CategoryOperations.Queries;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoryQuery, ApiResponse<List<CategoryResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<CategoryResponse>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
    {
        List<Category> list = unitOfWork.CategoryRepository.GetAll().Where(x => x.IsActive == true).ToList();

        List<CategoryResponse> mapped = mapper.Map<List<CategoryResponse>>(list);

        return new ApiResponse<List<CategoryResponse>>(mapped);
    }
}
