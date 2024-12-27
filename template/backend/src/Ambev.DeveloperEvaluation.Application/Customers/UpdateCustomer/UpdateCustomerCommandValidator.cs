using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(50);
        
        RuleFor(customer => customer.Age)
            .NotEmpty()
            .GreaterThan(0)
            .LessThan(150);

        RuleFor(customer => customer.UserId)
            .NotEmpty();
    }
}