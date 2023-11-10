using FluentValidation;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Validation;

public class PaymentValidator : AbstractValidator<PaymentRequest>
{
    public PaymentValidator()
    {
        RuleFor(x => x.PaymentDescription).NotEmpty().WithMessage("PaymentDescription is required");
        RuleFor(x => x.PaymentDescription).MaximumLength(50).WithMessage("The maximum length of the PaymentDescription line can be 50 characters.");

        RuleFor(x => x.PaymentType).NotEmpty().WithMessage("PaymentType is required");
    }
}
