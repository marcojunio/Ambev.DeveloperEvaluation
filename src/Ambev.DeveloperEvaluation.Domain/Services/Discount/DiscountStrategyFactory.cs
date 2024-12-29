using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Services.Discount.Implementation;

namespace Ambev.DeveloperEvaluation.Domain.Services.Discount;

public class DiscountStrategyFactory
{
    public static IDiscountStrategy GetStrategy(SaleItem item)
    {
        return item.Quantity switch
        {
            >= 10 and <= 20 => new TwentyPercentDiscountStrategy(),
            >= 4 => new TenPercentDiscountStrategy(),
            _ => new NoDiscountStrategy()
        };
    }
}