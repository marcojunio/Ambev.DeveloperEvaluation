using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
{
    public CreateSaleRequestValidator()
    {
        RuleFor(sale => sale.SellingCompanyId)
            .NotEmpty();

        RuleFor(sale => sale.CustomerId)
            .NotEmpty();

        RuleFor(sale => sale.SaleItems)
            .NotEmpty()
            .Must(items => items != null && items.Any()).WithMessage("Sale must contain at least one item")
            .ForEach(item => item.SetValidator(new SaleItemDtoValidator()));
    }
}