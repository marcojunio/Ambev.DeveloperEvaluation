using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct;

public class GetProductValidator : AbstractValidator<GetProductQuery>
{
    public GetProductValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Product ID is required");
    }
}