using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Company.DeleteCompany;

public class DeleteCompanyRequestValidator : AbstractValidator<DeleteCompanyRequest>
{
    public DeleteCompanyRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Company ID is required");
    }
}