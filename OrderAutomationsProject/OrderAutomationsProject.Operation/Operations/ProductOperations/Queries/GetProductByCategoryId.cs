using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.ProductOperations.Queries;

public class GetProductByCategoryIdQueryHandler : IRequestHandler<GetProductByCategoryIdQuery, ApiResponse<List<ProductResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public GetProductByCategoryIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<List<ProductResponse>>> Handle(GetProductByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        List<Product> list = unitOfWork.ProductRepository.Where(x => x.CategoryId == request.Id && x.IsActive == true, "Category").ToList();

        if (sessionService.CheckSession().sessionRole == "dealer")
        {
            var sessionid = sessionService.CheckSession().sessionId;
            decimal divident = unitOfWork.DealerRepository.GetById(sessionid).Dividend;
            list = ProductHelper.GetProductsWithDividend(list, divident);
        }

        List<ProductResponse> mapped = mapper.Map<List<ProductResponse>>(list);

        return new ApiResponse<List<ProductResponse>>(mapped);
    }
}
