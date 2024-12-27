using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public class SaleItemTestData
{
    private static readonly Faker<SaleItem> SaleItemFaker = new Faker<SaleItem>()
        .RuleFor(u => u.UserId, Guid.NewGuid)
        .RuleFor(u => u.Discount, f => f.Random.Decimal(0.1m))
        .RuleFor(u => u.Quantity, f => f.Random.Number(10,1000))
        .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(0.1m));

    public static SaleItem GenerateValidSaleItem()
    {
        var saleItem = SaleItemFaker.Generate();

        saleItem.Product = ProductTestData.GenerateValidProduct();
        return saleItem;
    }
    
    public static SaleItem GenerateRandomSaleItem()
    {
        var product = new Product()
        {
            UserId = Guid.NewGuid(),
            UnitPrice = new Faker().Random.Number(),
            Name = $"Tests{new Faker().Random.String(10)}"
        };
        
        product.IncreaseStock(300);
        
        var sale = new SaleItem()
        {
            UserId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            UnitPrice = new Faker().Random.Decimal(),
            Quantity = new Faker().Random.Number(1,200),
            Product = product
        };
        
        sale.ApplyDiscount(new Faker().Random.Decimal(0.1m));

        return sale;
    }
}