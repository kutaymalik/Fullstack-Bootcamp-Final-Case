using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.CostOperations.Commands;

public class ConfirmCostCommandHandler : IRequestHandler<ConfirmCostCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public ConfirmCostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse> Handle(ConfirmCostCommand request, CancellationToken cancellationToken)
    {
        // Finding required Cost to Cost Table
        Cost entity = await unitOfWork.CostRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null || entity.IsActive == false || entity.DealerId != sessionService.CheckSession().sessionId)
        {
            return new ApiResponse("Record not found!");
        }

        entity.Confirmation = true;
        unitOfWork.CostRepository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        // Returning response as ApiResponse type
        return new ApiResponse("Order confirmed.");
    }
}
