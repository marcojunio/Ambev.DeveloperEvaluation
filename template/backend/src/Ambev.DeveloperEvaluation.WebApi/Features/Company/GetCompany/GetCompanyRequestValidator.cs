using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.GetCompany;

public class GetCompanyRequestValidator : AbstractValidator<GetCompanyRequest>
{
    public GetCompanyRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Company ID is required");
    }
}