using AutoMapper;
using Azure;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Operation.Validation;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.AddressOperations.Commands;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, ApiResponse<AddressResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public CreateAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<AddressResponse>> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
            AddressValidator validator = new AddressValidator();
            var validationResult = validator.Validate(request.Model);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
                var res = new ApiResponse<AddressResponse>(errors);
                res.StatusCode = 400;
                res.Success = false;
                return res;
            }

            Address mapped = mapper.Map<Address>(request.Model);

            mapped.DealerId = sessionService.CheckSession().sessionId;

            await unitOfWork.AddressRepository.InsertAsync(mapped, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            var response = mapper.Map<AddressResponse>(mapped);

            return new ApiResponse<AddressResponse>(response);
    }
}
