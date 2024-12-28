using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services.Discount.Implementation;

public class NoDiscountStrategy : IDiscountStrategy
{
    public decimal CalculateDiscount(SaleItem item)
    {
        return 0;
    }
}