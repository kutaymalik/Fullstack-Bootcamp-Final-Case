using FluentValidation;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Validation;

public class AddressValidator : AbstractValidator<AddressRequest>
{
    public AddressValidator()
    {
        RuleFor(x => x.AddressLine1).NotEmpty().WithMessage("AddressLine1 is required");
        RuleFor(x => x.AddressLine1).MaximumLength(50).WithMessage("The maximum length of the address line can be 50 characters.");
        
        RuleFor(x => x.AddressLine2).NotEmpty().WithMessage("AddressLine2 is required");
        RuleFor(x => x.AddressLine2).MaximumLength(50).WithMessage("The maximum length of the address line can be 50 characters.");
       
        RuleFor(x => x.City).NotEmpty().WithMessage("City is required");
        RuleFor(x => x.City).MaximumLength(50).WithMessage("The maximum length of the city line can be 50 characters.");
        
        RuleFor(x => x.County).NotEmpty().WithMessage("County is required");
        RuleFor(x => x.County).MaximumLength(50).WithMessage("The maximum length of the county line can be 50 characters.");
        
        RuleFor(x => x.PostalCode).NotEmpty().WithMessage("PostalCode is required");
        RuleFor(x => x.PostalCode).Length(5).WithMessage("PostalCode must be 5 digits");
    }
}
