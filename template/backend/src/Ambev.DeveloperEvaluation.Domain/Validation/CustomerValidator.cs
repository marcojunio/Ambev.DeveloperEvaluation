using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(costumer => costumer.Name)
            .NotEmpty()
            .MinimumLength(6).WithMessage("Name must be at least 6 characters long.")
            .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters.");


        RuleFor(costumer => costumer.Age)
            .NotEmpty()
            .GreaterThan(0).WithMessage("Age must be greater than zero")
            .LessThan(150).WithMessage("Age must be less than 150");
        
        RuleFor(company => company.UserId)
            .NotEmpty().WithMessage("User required");
    }
}