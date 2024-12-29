using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services.Discount.Implementation;

public class TwentyPercentDiscountStrategy : IDiscountStrategy
{
    public decimal CalculateDiscount(SaleItem item)
    {
        return item.Quantity is >= 10 and <= 20 ? 0.20m : 0;
    }
}