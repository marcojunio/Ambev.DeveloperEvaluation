using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Product.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(product => product.Name)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(50);

        RuleFor(product => product.UnitPrice)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(product => product.StockQuantity)
            .NotEmpty()
            .GreaterThan(0);

        RuleFor(product => product.UserId)
            .NotEmpty();
    }
}