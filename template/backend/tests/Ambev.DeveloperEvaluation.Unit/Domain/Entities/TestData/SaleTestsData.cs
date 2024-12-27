using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public class SaleTestsData
{
    private static readonly Faker<Sale> SaleFaker = new Faker<Sale>()
        .RuleFor(u => u.UserId, Guid.NewGuid)
        .RuleFor(u => u.SellingCompanyId, Guid.NewGuid)
        .RuleFor(u => u.CustomerId, Guid.NewGuid);
    
    public static Sale GenerateValidSale()
    {
        var sale = SaleFaker.Generate();
    
        sale.AddItem(SaleItemTestData.GenerateRandomSaleItem());
        sale.AddItem(SaleItemTestData.GenerateRandomSaleItem());

        return sale;
    }
}