using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.CostOperations.Commands;

public class DeleteCostCommandHandler : IRequestHandler<DeleteCostCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public DeleteCostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse> Handle(DeleteCostCommand request, CancellationToken cancellationToken)
    {
        // Finding Cost to delete
        Cost entity = await unitOfWork.CostRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null || entity.IsActive == false || entity.DealerId != sessionService.CheckSession().sessionId)
        {
            var res = new ApiResponse("Record not found!");
            res.Success = false;
            return res;
        }

        // Changing entity state
        entity.IsActive = false;

        // Saving changes to the database
        await unitOfWork.CompleteAsync(cancellationToken);

        // Returning response as ApiResponse type
        return new ApiResponse();
    }
}
