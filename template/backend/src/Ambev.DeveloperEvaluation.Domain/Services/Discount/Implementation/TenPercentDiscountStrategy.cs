using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services.Discount.Implementation;

public class TenPercentDiscountStrategy : IDiscountStrategy
{
    public decimal CalculateDiscount(SaleItem item)
    {
        return item.Quantity is >= 4 and < 10 ? 0.10m : 0;
    }
}