using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using OrderAutomationsProject.Schema;

namespace OrderAutomationsProject.Operation.Validation;

public class DealerValidator : AbstractValidator<DealerRequest>
{
    public DealerValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required");
        RuleFor(x => x.FirstName).MaximumLength(40).WithMessage("The maximum length of the FirstName line can be 40 characters.");

        RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
        RuleFor(x => x.LastName).MaximumLength(40).WithMessage("The maximum length of the LastName line can be 40 characters.");

        RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
        RuleFor(x => x.Email).MaximumLength(100).WithMessage("The maximum length of the Email line can be 100 characters.");

        RuleFor(x => x.PasswordHash).NotEmpty().WithMessage("Password is required");
        RuleFor(x => x.PasswordHash).MaximumLength(30).WithMessage("The maximum length of the Password line can be 30 characters.");

        RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("PhoneNumber is required");
        RuleFor(x => x.PhoneNumber).MaximumLength(12).WithMessage("The maximum length of the PhoneNumber line can be 12 characters.");

        RuleFor(x => x.Dividend).NotEmpty().WithMessage("Dividend is required");
        RuleFor(x => x.Dividend).LessThan(100).WithMessage("Dividend must be less than hundred");

        RuleFor(x => x.OpenAccountLimit).NotEmpty().WithMessage("Dividend is required");
        RuleFor(x => x.OpenAccountLimit).GreaterThan(0).WithMessage("OpenAccountLimit must be greater than zero");
    }
}
