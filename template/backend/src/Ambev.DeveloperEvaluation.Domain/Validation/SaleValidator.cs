using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleValidator : AbstractValidator<Sale>
{
    public SaleValidator()
    {
        RuleFor(company => company.UserId)
            .NotEmpty().WithMessage("User required");
        
        RuleFor(company => company.SellingCompanyId)
            .NotEmpty().WithMessage("Selling company required");
        
        RuleFor(company => company.CustomerId)
            .NotEmpty().WithMessage("Customer required");
    }
}