using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;
using System.Threading.Channels;

namespace OrderAutomationsProject.Operation.Operations.AddressOperations.Commands;

public class DeleteAddressCommandHandler : IRequestHandler<DeleteAddressCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public DeleteAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse> Handle(DeleteAddressCommand request, CancellationToken cancellationToken)
    {
        // Finding address to delete
        Address entity = await unitOfWork.AddressRepository.GetByIdAsync(request.Id, cancellationToken);

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
