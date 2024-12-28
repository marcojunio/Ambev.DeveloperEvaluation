using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

public class UpdateSaleCommandValidator : AbstractValidator<UpdateSaleCommand>
{
    public UpdateSaleCommandValidator()
    {
        RuleFor(sale => sale.CustomerId)
            .NotEmpty();
        
        RuleFor(sale => sale.SellingCompanyId)
            .NotEmpty();

        RuleFor(sale => sale.SaleNumber)
            .NotEmpty().NotNull().Length(1, 15);

        RuleFor(sale => sale.SaleItems)
            .NotEmpty()
            .Must(items => items != null && items.Any()).WithMessage("Sale must contain at least one item")
            .ForEach(item => item.SetValidator(new SaleItemUpdateDtoValidator()));
    }
}