using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.UpdateCompany;

public class UpdateCompanyRequestValidator : AbstractValidator<UpdateCompanyRequest>
{
    public UpdateCompanyRequestValidator()
    {
        RuleFor(company => company.Id)
            .NotEmpty();
        
        RuleFor(company => company.Name)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(50);
    }
}