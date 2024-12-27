using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CompanyValidator : AbstractValidator<Company>
{
    public CompanyValidator()
    {
        RuleFor(company => company.Name)
            .NotEmpty()
            .MinimumLength(6).WithMessage("Name must be at least 6 characters long.")
            .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters.");

        RuleFor(company => company.UserId)
            .NotEmpty().WithMessage("User required");
    }
}