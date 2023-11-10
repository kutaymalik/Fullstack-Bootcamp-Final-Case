using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.Bank;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Operation.Validation;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.CardOperations.Commands;

public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, ApiResponse<CardResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public CreateCardCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService; 
    }

    public async Task<ApiResponse<CardResponse>> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        var card = unitOfWork.CardRepository.FirstOrDefault(x => x.CardNumber == request.Model.CardNumber);

        if(card != null)
        {
            throw new InvalidOperationException("This card number already exists!");
        }

        CardValidator validator = new CardValidator();
        var validationResult = validator.Validate(request.Model);

        if (!validationResult.IsValid)
        {
            var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
            var res = new ApiResponse<CardResponse>(errors);
            res.StatusCode = 400;
            res.Success = false;
            return res;
        }

        Card mapped = mapper.Map<Card>(request.Model);

        mapped.DealerId = sessionService.CheckSession().sessionId;

        var bankname = GetBankNameFromCardNumber(mapped.CardNumber);

        mapped.BankName = bankname;

        await unitOfWork.CardRepository.InsertAsync(mapped, cancellationToken);
        await unitOfWork.CompleteAsync(cancellationToken);

        var response = mapper.Map<CardResponse>(mapped);

        return new ApiResponse<CardResponse>(response);
    }


    private static BankName GetBankNameFromCardNumber(string cardNumber)
    {
        if (cardNumber.Length != 14)
        {
            throw new ArgumentException("The card number must be 14 digits!");
        }

        if (cardNumber.StartsWith("455"))
        {
            return BankName.XBank;
        }
        else if (cardNumber.StartsWith("543"))
        {
            return BankName.YBank;
        }
        else if (cardNumber.StartsWith("479"))
        {
            return BankName.ZBank;
        }
        else
        {
            throw new ArgumentException("Geçersiz kart numarası.");
        }
    }
}

