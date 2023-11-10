using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OrderAutomationsProject.Operation.Operations.AddressOperations.Commands;

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public UpdateAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        // Finding required address to Address Table
        Address entity = await unitOfWork.AddressRepository.GetByIdAsync(request.Id, cancellationToken);

        if (entity == null || entity.DealerId != sessionService.CheckSession().sessionId)
        {
            var res = new ApiResponse("Record not found!");
            res.Success = false;
            return res;
        }

        // Changing required fields
        entity.AddressLine1 = (string.IsNullOrEmpty(request.Model.AddressLine1.Trim()) || request.Model.AddressLine1.ToLower() == "string") ? entity.AddressLine1 : request.Model.AddressLine1;
        entity.AddressLine2 = (string.IsNullOrEmpty(request.Model.AddressLine2.Trim()) || request.Model.AddressLine2.ToLower() == "string") ? entity.AddressLine2 : request.Model.AddressLine2;
        entity.City = (string.IsNullOrEmpty(request.Model.City.Trim()) || request.Model.City.ToLower() == "string") ? entity.City : request.Model.City;
        entity.County = (string.IsNullOrEmpty(request.Model.County.Trim()) || request.Model.County.ToLower() == "string") ? entity.County : request.Model.County;
        entity.PostalCode = (string.IsNullOrEmpty(request.Model.PostalCode.Trim()) || request.Model.PostalCode.ToLower() == "string") ? entity.PostalCode : request.Model.PostalCode;

        unitOfWork.AddressRepository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        // Returning response as ApiResponse type
        
        return new ApiResponse();
    }
}
