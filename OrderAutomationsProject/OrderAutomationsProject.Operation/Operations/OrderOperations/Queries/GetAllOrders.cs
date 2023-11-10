using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.OrderOperations.Queries;

public class GetAllOrders : IRequestHandler<GetAllOrderQuery, ApiResponse<List<OrderResponse>>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public GetAllOrders(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<OrderResponse>>> Handle(GetAllOrderQuery request, CancellationToken cancellationToken)
    {
        List<Order> list = unitOfWork.OrderRepository.GetAll("OrderItems").Where(x => x.IsActive == true).ToList();

        List<OrderResponse> mapped = mapper.Map<List<OrderResponse>>(list);

        return new ApiResponse<List<OrderResponse>>(mapped);
    }
}
