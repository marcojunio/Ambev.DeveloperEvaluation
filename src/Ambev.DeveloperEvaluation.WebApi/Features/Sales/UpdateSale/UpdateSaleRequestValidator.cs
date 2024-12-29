using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequestValidator : AbstractValidator<UpdateSaleRequest>
{
    public UpdateSaleRequestValidator()
    {
        RuleFor(sale => sale.SaleNumber)
            .NotEmpty().NotNull().Length(1, 15);
        
        RuleFor(sale => sale.CustomerId)
            .NotEmpty();
        
        RuleFor(sale => sale.SellingCompanyId)
            .NotEmpty();

        RuleFor(sale => sale.SaleItems)
            .NotEmpty()
            .Must(items => items != null && items.Any()).WithMessage("Sale must contain at least one item")
            .ForEach(item => item.SetValidator(new SaleItemUpdateDtoValidator()));
    }
}