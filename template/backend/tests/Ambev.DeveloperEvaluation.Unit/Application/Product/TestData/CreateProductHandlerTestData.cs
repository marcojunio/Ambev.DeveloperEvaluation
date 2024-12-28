using Ambev.DeveloperEvaluation.Application.Product.CreateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product.TestData;

public static class CreateProductHandlerTestData
{
    private static readonly Faker<CreateProductCommand> CreateProductHandlerFaker = new Faker<CreateProductCommand>()
        .RuleFor(u => u.UserId, f => Guid.NewGuid())
        .RuleFor(u => u.Name, f => f.Random.String(15))
        .RuleFor(u => u.StockQuantity, f => f.Random.Number(5,50))
        .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(2,1000));

    public static CreateProductCommand GenerateValidCommand()
    {
        return CreateProductHandlerFaker.Generate();
    }
}