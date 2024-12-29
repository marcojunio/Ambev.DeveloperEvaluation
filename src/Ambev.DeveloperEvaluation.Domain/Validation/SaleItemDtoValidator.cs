using Ambev.DeveloperEvaluation.Domain.Dtos;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemDtoValidator : AbstractValidator<SaleItemDto>
{
    public SaleItemDtoValidator()
    {
        RuleFor(e => e.Quantity)
            .NotNull().WithMessage("Quantity is required")
            .GreaterThan(0).WithMessage("Quantity be greater than zero");

        RuleFor(e => e.ProductId)
            .NotEmpty()
            .WithMessage("Product is required");
        
        RuleFor(e => e.UnitPrice)
            .NotNull().WithMessage("Quantity is required")
            .GreaterThan(0).WithMessage("Unit price should be greater than zero");
    }
}