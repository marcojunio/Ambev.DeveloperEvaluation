using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale;

public class GetSaleValidator : AbstractValidator<GetSaleQuery>
{
    public GetSaleValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required");
    }
}