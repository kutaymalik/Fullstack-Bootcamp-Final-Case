using FluentValidation;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Validation;

public class CostValidator : AbstractValidator<CostRequest>
{
    public CostValidator()
    {
        RuleFor(x => x.CostDescription).NotEmpty().MaximumLength(30).WithMessage("CardNumber is required");
        RuleFor(x => x.CostAmount).NotEmpty().GreaterThan(0).WithMessage("Cost amount cannot be zero");
    }
}
