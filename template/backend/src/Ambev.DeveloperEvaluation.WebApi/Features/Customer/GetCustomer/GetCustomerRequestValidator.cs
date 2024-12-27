using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customer.GetCustomer;

public class GetCustomerRequestValidator : AbstractValidator<GetCustomerRequest>
{
    public GetCustomerRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Customer ID is required");
    }
}