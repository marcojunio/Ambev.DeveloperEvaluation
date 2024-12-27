using Ambev.DeveloperEvaluation.Common.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;

public class CreateCompanyCommand : IRequest<CreateCompanyResult>
{
    public string Name { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    
    public ValidationResultDetail Validate()
    {
        var validator = new CreateCompanyCommandValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}