using FluentValidation;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Validation;

public class ProductValidator : AbstractValidator<ProductRequest>
{
    public ProductValidator()
    {
        RuleFor(x => x.ProductName).MaximumLength(50).WithMessage("The maximum length of the ProductName line can be 50 characters.");
        RuleFor(x => x.ProductName).NotEmpty().WithMessage("ProductName is required");

        RuleFor(x => x.ProductDescription).NotEmpty().WithMessage("ProductDescription is required");
        RuleFor(x => x.ProductDescription).MaximumLength(100).WithMessage("The maximum length of the ProductDescription line can be 100 characters.");

        RuleFor(x => x.Price).NotEmpty().WithMessage("Price is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must greater than zero");

        RuleFor(x => x.StockQuantity).NotEmpty().WithMessage("StockQuantity is required");
        RuleFor(x => x.StockQuantity).GreaterThan(0).WithMessage("StockQuantity must greater than zero");
    }
}
