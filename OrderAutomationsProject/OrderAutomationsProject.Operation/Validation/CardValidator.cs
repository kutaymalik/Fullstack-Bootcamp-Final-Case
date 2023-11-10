using FluentValidation;
using OrderAutomationsProject.Operation.Cqrs;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Validation;

public class CardValidator : AbstractValidator<CardRequest>
{
    public CardValidator()
    {
        RuleFor(x => x.CardNumber).NotEmpty().Length(14).WithMessage("CardNumber is required");
        RuleFor(x => x.CardNumber).Length(14).WithMessage("CardNumber must be 14 digits");

        RuleFor(x => x.ExpiryDate).NotEmpty().WithMessage("ExpiryDate is required");
        RuleFor(x => x.ExpiryDate).Length(5).WithMessage("ExpiryDate must be 5 digits");

        RuleFor(x => x.CVV).NotEmpty().WithMessage("CVV is required");
        RuleFor(x => x.CVV).Length(3).WithMessage("CVV must be 3 digits");

        RuleFor(x => x.ExpenseLimit).NotEmpty().GreaterThan(0).WithMessage("ExpenseLimit is required");
        RuleFor(x => x.ExpenseLimit).GreaterThan(0).WithMessage("ExpenseLimit must greater than zero");

        RuleFor(x => x.CardHolderType).NotEmpty().MaximumLength(6).WithMessage("CardHolderType is required");
    }
}
