using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(product => product.UnitPrice)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Unit price must be greater than zero");
        
        RuleFor(x => x.Discount)
            .Must(value => value is null or > 0)
            .GreaterThan(0).WithMessage("Unit price must be greater than zero");
        
        RuleFor(x => x.Quantity)
            .GreaterThan(0).WithMessage("Quantity be greater than zero");
        
        RuleFor(company => company.UserId)
            .NotEmpty().WithMessage("User required");
    }
}