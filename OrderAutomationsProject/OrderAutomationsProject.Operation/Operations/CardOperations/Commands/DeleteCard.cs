using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.CardOperations.Commands;

public class DeleteCardCommandHandler : IRequestHandler<DeleteCardCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public DeleteCardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse> Handle(DeleteCardCommand request, CancellationToken cancellationToken)
    {
        // Finding Card to delete
        Card entity = await unitOfWork.CardRepository.GetByIdAsync(request.Id, cancellationToken);

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
