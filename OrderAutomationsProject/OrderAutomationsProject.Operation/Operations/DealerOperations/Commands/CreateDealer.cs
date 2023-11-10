using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Encryption;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Operation.Validation;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.DealerOperations.Commands;

public class CreateDealerCommandHandler : IRequestHandler<CreateDealerCommand, ApiResponse<DealerResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;

    public CreateDealerCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<DealerResponse>> Handle(CreateDealerCommand request, CancellationToken cancellationToken)
    {
        DealerValidator validator = new DealerValidator();
        var validationResult = validator.Validate(request.Model);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            var res = new ApiResponse<DealerResponse>(errors);
            res.StatusCode = 400;
            res.Success = false;
            return res;
        }


        var dealer = unitOfWork.DealerRepository.FirstOrDefault(x => x.Email == request.Model.Email);

        if(dealer != null)
        {
            throw new InvalidOperationException("There is already a user with this email!");
        }

        Dealer mapped = mapper.Map<Dealer>(request.Model);

        mapped.PasswordHash = Md5.Create(request.Model.PasswordHash).ToLower();
        mapped.DealerNumber = Guid.NewGuid().ToString().Replace("-", "").Substring(1, 6).ToLower();

        await unitOfWork.DealerRepository.InsertAsync(mapped, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        var response = mapper.Map<DealerResponse>(mapped);

        return new ApiResponse<DealerResponse>(response);
    }
}
