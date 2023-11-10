using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;

namespace OrderAutomationsProject.Operation.Operations.DealerOperations.Commands;

public class UpdateDealerCommandHandler : IRequestHandler<UpdateDealerCommand, ApiResponse>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public UpdateDealerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse> Handle(UpdateDealerCommand request, CancellationToken cancellationToken)
    {
        // Finding required Dealer to Dealer Table
        Dealer entity = await unitOfWork.DealerRepository.GetByIdAsync(request.Id, cancellationToken);

        if(sessionService.CheckSession().sessionRole == "dealer" && (entity == null || entity.Id != sessionService.CheckSession().sessionId))
        {
            var res = new ApiResponse("Record not found!");
            res.Success = false;
            return res;
        }

        // Changing required fields
        entity.FirstName = (string.IsNullOrEmpty(request.Model.FirstName.Trim()) || request.Model.FirstName.ToLower() == "string") ? entity.FirstName : request.Model.FirstName;
        entity.LastName = (string.IsNullOrEmpty(request.Model.LastName.Trim()) || request.Model.LastName.ToLower() == "string") ? entity.LastName : request.Model.LastName;
        entity.Email = (string.IsNullOrEmpty(request.Model.Email.Trim()) || request.Model.Email.ToLower() == "string") ? entity.Email : request.Model.Email;
        entity.PasswordHash = (string.IsNullOrEmpty(request.Model.PasswordHash.Trim()) || request.Model.PasswordHash.ToLower() == "string") ? entity.PasswordHash : request.Model.PasswordHash;

        entity.DateOfBirth = (request.Model.DateOfBirth == default(DateTime) &&
            request.Model.DateOfBirth == DateTime.MinValue &&
            request.Model.DateOfBirth >= DateTime.Today) ? entity.DateOfBirth : request.Model.DateOfBirth;
        entity.PhoneNumber = string.IsNullOrEmpty(request.Model.PhoneNumber.Trim()) ? entity.Role : request.Model.PhoneNumber;
        entity.Dividend = (request.Model.Dividend != default && request.Model.Dividend != 0) ? request.Model.Dividend : entity.Dividend;
        entity.OpenAccountLimit = (request.Model.OpenAccountLimit != default && request.Model.OpenAccountLimit != 0) ? request.Model.OpenAccountLimit : entity.OpenAccountLimit;

        //entity.DateOfBirth = (request.Model.DateOfBirth.HasValue && request.Model.DateOfBirth >= DateTime.Today)
        //    ? entity.DateOfBirth
        //    : request.Model.DateOfBirth ?? entity.DateOfBirth;

        //entity.PhoneNumber = string.IsNullOrEmpty(request.Model.PhoneNumber?.Trim())
        //    ? entity.PhoneNumber
        //    : request.Model.PhoneNumber;

        //entity.Dividend = (request.Model.Dividend.HasValue && request.Model.Dividend != 0)
        //    ? request.Model.Dividend.Value
        //: entity.Dividend;

        //entity.OpenAccountLimit = (request.Model.OpenAccountLimit.HasValue && request.Model.OpenAccountLimit != 0)
        //    ? request.Model.OpenAccountLimit.Value
        //    : entity.OpenAccountLimit;


        unitOfWork.DealerRepository.Update(entity);
        await unitOfWork.CompleteAsync(cancellationToken);

        // Returning response as ApiResponse type
        return new ApiResponse();
    }
}
