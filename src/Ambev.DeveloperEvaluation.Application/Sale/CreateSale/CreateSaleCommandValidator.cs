using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    public CreateSaleCommandValidator()
    {
        RuleFor(sale => sale.UserId)
            .NotEmpty();

        RuleFor(sale => sale.SellingCompanyId)
            .NotEmpty();

        RuleFor(sale => sale.CustomerId)
            .NotEmpty();

        RuleFor(sale => sale.UserId)
            .NotEmpty();

        RuleFor(sale => sale.SaleItems)
            .NotEmpty()
            .Must(items => items != null && items.Any()).WithMessage("Sale must contain at least one item")
            .ForEach(item => item.SetValidator(new SaleItemDtoValidator()));
    }
}