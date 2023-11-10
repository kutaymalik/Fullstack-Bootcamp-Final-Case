using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;
using System.Collections.Generic;

namespace OrderAutomationsProject.Operation.Operations.ProductOperations.Queries;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ApiResponse<ProductResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public GetProductByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<ProductResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product entity = await unitOfWork.ProductRepository.GetByIdAsync(request.Id, cancellationToken, "Category");

        if (sessionService.CheckSession().sessionRole == "dealer")
        {
            var sessionid = sessionService.CheckSession().sessionId;
            decimal divident = unitOfWork.DealerRepository.GetById(sessionid).Dividend;
            entity.Price += (entity.Price * divident / 100);
        }

        if (entity == null || entity.IsActive == false)
        {
            return new ApiResponse<ProductResponse>("Record not found!");
        }

        ProductResponse mapped = mapper.Map<ProductResponse>(entity);

        return new ApiResponse<ProductResponse>(mapped);
    }
}
