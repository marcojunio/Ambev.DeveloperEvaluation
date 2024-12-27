using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class ProductTestData
{
    private static readonly Faker<Product> ProductFaker = new Faker<Product>()
        .RuleFor(u => u.UserId, Guid.NewGuid)
        .RuleFor(u => u.Name, f => f.Name.Random.ToString())
        .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(0.1m));

    public static Product GenerateValidProduct()
    {
        var product = ProductFaker.Generate();

        product.IncreaseStock(500);

        return product;
    }

    public static string GenerateInvalidLongName()
    {
        return new Faker().Random.String(51);
    }

    public static string GenerateInvalidMinName()
    {
        return new Faker().Random.String(5);
    }

    public static int GenerateInvalidMinUnitPrice()
    {
        return -1;
    }
}