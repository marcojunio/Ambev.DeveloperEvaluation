using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.CreateCompany;

public class CreateCompanyRequestValidator : AbstractValidator<CreateCompanyRequest>
{
    public CreateCompanyRequestValidator()
    {
        RuleFor(company => company.Name)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(50);
    }
}