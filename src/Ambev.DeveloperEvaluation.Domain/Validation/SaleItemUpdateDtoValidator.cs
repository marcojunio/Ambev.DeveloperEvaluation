using Ambev.DeveloperEvaluation.Domain.Dtos;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemUpdateDtoValidator : AbstractValidator<SaleItemUpdateDto>
{
    public SaleItemUpdateDtoValidator()
    {
        RuleFor(e => e.Quantity)
            .NotNull().WithMessage("Quantity is required")
            .GreaterThan(0).WithMessage("Quantity be greater than zero");
        
        RuleFor(e => e.UnitPrice)
            .NotNull().WithMessage("Unit price is required")
                .GreaterThan(0).WithMessage("Unit price be greater than zero");

        RuleFor(e => e.ProductId)
            .NotEmpty()
            .WithMessage("Product is required");
    }
}