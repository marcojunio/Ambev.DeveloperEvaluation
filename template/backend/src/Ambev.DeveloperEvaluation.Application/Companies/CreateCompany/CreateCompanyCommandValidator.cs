using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;

public class CreateCompanyCommandValidator : AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        RuleFor(company => company.Name)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(50);

        RuleFor(company => company.UserId)
            .NotEmpty();
    }
}