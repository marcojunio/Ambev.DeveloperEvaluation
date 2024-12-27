using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .MinimumLength(6).WithMessage("Name must be at least 6 characters long.")
            .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters.");

        RuleFor(product => product.UnitPrice)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Unit price must be greater than zero");
        
        RuleFor(company => company.UserId)
            .NotEmpty().WithMessage("User required");
    }
}