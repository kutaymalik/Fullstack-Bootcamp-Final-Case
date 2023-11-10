using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.DealerOperations.Commands;

public class DeleteDealerCommandHandler : IRequestHandler<DeleteDealerCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public DeleteDealerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse> Handle(DeleteDealerCommand request, CancellationToken cancellationToken)
    {
        // Finding Dealer to delete
        Dealer entity = await unitOfWork.DealerRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null || entity.IsActive == false)
        {
            var res = new ApiResponse("Record not found!");
            res.Success = false;
            return res;
        }

        if (entity.Addresses.Any() || entity.Orders.Any() || entity.Cards.Any() || entity.Payments.Any())
        {
            var res = new ApiResponse("This dealer contains other models!");
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
