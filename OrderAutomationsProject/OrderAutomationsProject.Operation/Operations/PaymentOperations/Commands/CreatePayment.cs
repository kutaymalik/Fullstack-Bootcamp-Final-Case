using AutoMapper;
using MediatR;
using OrderAutomationsProject.Base.CardHolder;
using OrderAutomationsProject.Base.PaymentType;
using OrderAutomationsProject.Base.Response;
using OrderAutomationsProject.Data.Domain;
using OrderAutomationsProject.Data.Helpers;
using OrderAutomationsProject.Data.UnitOfWorks;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Operation.Validation;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Operations.PaymentOperations.Commands;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, ApiResponse<PaymentResponse>>
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IMapper mapper;
    private readonly ISessionService sessionService;

    public CreatePaymentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ISessionService sessionService)
    {
        this.unitOfWork = unitOfWork;
        this.mapper = mapper;
        this.sessionService = sessionService;
    }

    public async Task<ApiResponse<PaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        try
        {
            PaymentValidator validator = new PaymentValidator();
            var validationResult = validator.Validate(request.Model);

            if (!validationResult.IsValid)
            {
                var errors = string.Join(", ", validationResult.Errors.Select(error => error.ErrorMessage));
                var res = new ApiResponse<PaymentResponse>(errors);
                res.StatusCode = 400;
                res.Success = false;
                return res;
            }

            Order order = unitOfWork.OrderRepository.FirstOrDefault(x => x.Id == request.Model.OrderId);
            Card card = unitOfWork.CardRepository.FirstOrDefault(x => x.CardNumber == request.Model.CardNumber);

            if (order.Confirmation == false)
            {
                throw new ArgumentException("You can pay if the order is confirmed by admin!");
            }

            ValidatePaymentType(FromString(request.Model.PaymentType), card, order);

            Payment payment = CreatePayment(request.Model, order, card);

            ProcessPayment(order, card, FromString(request.Model.PaymentType), payment);

            await unitOfWork.PaymentRepository.InsertAsync(payment, cancellationToken);
            await unitOfWork.CompleteAsync(cancellationToken);

            var response = mapper.Map<PaymentResponse>(payment);

            return new ApiResponse<PaymentResponse>(response);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Hata: " + ex.Message);
            if (ex.InnerException != null)
            {
                Console.WriteLine("İç Hata: " + ex.InnerException.Message);
            }
            return new ApiResponse<PaymentResponse>(ex.Message);
        }
    }

    private Payment CreatePayment(PaymentRequest model, Order order, Card card)
    {
        return new Payment
        {
            PaymentType = FromString(model.PaymentType),
            PaymentAmount = order.TotalAmount,
            PaymentDate = DateTime.UtcNow,
            PaymentDescription = model.PaymentDescription,
            OrderId = order.Id,
            CardId = card.Id,
        };
    }

    public static PaymentTypeName FromString(string paymentTypeString)
    {
        if (Enum.TryParse(paymentTypeString, out PaymentTypeName paymentType))
        {
            return paymentType;
        }

        throw new ArgumentException("Invalid payment type: " + paymentTypeString);
    }


    private void ValidatePaymentType(PaymentTypeName paymentType, Card card, Order order)
    {
        if(order.PaymentStatus == true)
        {
            throw new InvalidOperationException("Order payment made!");
        }

        if ((paymentType == PaymentTypeName.CreditCardPayment || paymentType == PaymentTypeName.EftPayment || paymentType == PaymentTypeName.RemittancePayment) && card == null)
        {
            throw new InvalidOperationException("You must enter your card information to make a payment!");
        }

        if ((paymentType == PaymentTypeName.EftPayment || paymentType == PaymentTypeName.RemittancePayment) && card.CardHolderType.ToString() != "Debit")
        {
            throw new InvalidOperationException("To pay with EFT or Remittance, your card must be a debit card!");
        }

        if (paymentType == PaymentTypeName.CreditCardPayment && card.CardHolderType.ToString() != "Credit")
        {
            throw new InvalidOperationException("To pay with Credit Card Payment your card must be a credit card!");
        }

        if ((paymentType == PaymentTypeName.EftPayment || paymentType == PaymentTypeName.RemittancePayment) && card.ExpenseLimit < order.TotalAmount)
        {
            throw new InvalidOperationException("There is not enough balance on your card!");
        }

        if (paymentType == PaymentTypeName.CreditCardPayment && card.ExpenseLimit < order.TotalAmount)
        {
            throw new InvalidOperationException("There is not enough limit on your card!");
        }
    }

    private void ProcessPayment(Order order, Card card, PaymentTypeName paymentType, Payment payment)
    {
        if (paymentType == PaymentTypeName.EftPayment)
        {
            card.ExpenseLimit -= order.TotalAmount + 4; // EFT fee is 4 TL
        }
        else if (paymentType == PaymentTypeName.RemittancePayment)
        {
            card.ExpenseLimit -= order.TotalAmount;
        }
        else if (paymentType == PaymentTypeName.CreditCardPayment)
        {
            card.ExpenseLimit -= (order.TotalAmount + (order.TotalAmount * 326 / 10000)); // The commission rate on shopping is 3.26 percent
        }

        unitOfWork.CardRepository.Update(card);
        order.PaymentStatus = true;
        unitOfWork.OrderRepository.Update(order);
    }
}
