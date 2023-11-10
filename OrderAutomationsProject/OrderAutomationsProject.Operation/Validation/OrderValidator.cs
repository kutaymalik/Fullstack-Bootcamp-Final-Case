using FluentValidation;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Validation;

public class OrderValidator : AbstractValidator<OrderRequest>
{
    public OrderValidator()
    {
        RuleFor(x => x.OrderItems).NotEmpty().WithMessage("OrderItems is required");
    }
}
