using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Operation.Validation;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.CostOperations.Commands;

public class CreateCostCommandHandler : IRequestHandler<CreateCostCommand, ApiResponse<CostResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public CreateCostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<CostResponse>> Handle(CreateCostCommand request, CancellationToken cancellationToken)
    {
        CostValidator validator = new CostValidator();
        var validationResult = validator.Validate(request.Model);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            var res = new ApiResponse<CostResponse>(errors);
            res.StatusCode = 400;
            res.Success = false;
            return res;
        }

        Cost mapped = mapper.Map<Cost>(request.Model);

        mapped.DealerId = sessionService.CheckSession().sessionId;

        await unitOfWork.CostRepository.InsertAsync(mapped, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        var response = mapper.Map<CostResponse>(mapped);

        return new ApiResponse<CostResponse>(response);
    }
}