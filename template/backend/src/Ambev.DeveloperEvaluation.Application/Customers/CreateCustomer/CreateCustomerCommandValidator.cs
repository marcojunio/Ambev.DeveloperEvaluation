using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
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