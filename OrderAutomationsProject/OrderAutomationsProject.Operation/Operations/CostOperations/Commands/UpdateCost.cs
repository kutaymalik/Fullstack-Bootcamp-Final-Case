using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.CostOperations.Commands;

public class UpdateCostCommandHandler : IRequestHandler<UpdateCostCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public UpdateCostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse> Handle(UpdateCostCommand request, CancellationToken cancellationToken)
    {
        // Finding required Cost to Cost Table
        Cost entity = await unitOfWork.CostRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null || entity.DealerId != sessionService.CheckSession().sessionId)
        {
            var res = new ApiResponse("Record not found!");
            res.Success = false;
            return res;
        }

        // Changing required fields
        entity.CostDescription = (string.IsNullOrEmpty(request.Model.CostDescription.Trim()) || request.Model.CostDescription.ToLower() == "string") ? entity.CostDescription : request.Model.CostDescription;
        entity.CostAmount = (request.Model.CostAmount != default && request.Model.CostAmount != 0) ? request.Model.CostAmount : entity.CostAmount;
        entity.Confirmation = request.Model.Confirmation;

        unitOfWork.CostRepository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        // Returning response as ApiResponse type
        return new ApiResponse();
    }
}
