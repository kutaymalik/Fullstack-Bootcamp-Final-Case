using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Schema;
using OrderItemAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.OrderItemOperations.Commands;

public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, ApiResponse<OrderItemResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateOrderItemCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public Task<ApiResponse<OrderItemResponse>> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        OrderItem mapped = mapper.Map<OrderItem>(request.Model);

        unitOfWork.OrderItemRepository.InsertAsync(mapped, cancellationToken);
        unitOfWork.CompleteAsync(cancellationToken);

        var response = mapper.Map<OrderItemResponse>(mapped);

        return Task.FromResult(new ApiResponse<OrderItemResponse>(response));
    }
}
