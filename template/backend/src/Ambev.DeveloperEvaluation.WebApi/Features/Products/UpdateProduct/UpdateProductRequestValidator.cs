using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;

public class UpdateProductRequestValidator : AbstractValidator<UpdateProductRequest>
{
    public UpdateProductRequestValidator()
    {
        RuleFor(customer => customer.Name)
            .NotEmpty()
            .MinimumLength(6)
            .MaximumLength(50);

        RuleFor(product => product.UnitPrice)
            .NotEmpty()
            .GreaterThan(0);
        
        RuleFor(product => product.StockQuantity)
            .NotEmpty()
            .GreaterThan(0);
    }
}