using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

public class GetCustomerValidator : AbstractValidator<GetCustomerQuery>
{
    public GetCustomerValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Customer ID is required");
    }
}