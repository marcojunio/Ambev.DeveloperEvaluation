using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customer.CreateCustomer;

public class CreateCustomerRequestValidator : AbstractValidator<CreateCustomerRequest>
{
    public CreateCustomerRequestValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(50);

        RuleFor(customer => customer.Age)
            .NotEmpty()
            .GreaterThan(0)
            .LessThan(150);
    }
}