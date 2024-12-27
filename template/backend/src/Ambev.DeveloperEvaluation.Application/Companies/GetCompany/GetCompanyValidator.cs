using Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Companies.GetCompany;

public class GetCompanyValidator : AbstractValidator<GetCompanyCommand>
{
    public GetCompanyValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Company ID is required");
    }
}