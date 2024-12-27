using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customer.UpdateCustomer;

public class UpdateCustomerRequestValidator : AbstractValidator<UpdateCustomerRequest>
{
    public UpdateCustomerRequestValidator()
    {
        RuleFor(customer => customer.Id)
            .NotEmpty();

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