using Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;
using Ambev.DeveloperEvaluation.Application.Product.UpdateProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product.TestData;

public static class UpdateProductHandlerTestData
{
    private static readonly Faker<UpdateProductCommand> UpdateProductHandlerFaker = new Faker<UpdateProductCommand>()
        .RuleFor(u => u.Id, Guid.NewGuid)
        .RuleFor(u => u.UserId, f => Guid.NewGuid())
        .RuleFor(u => u.Name, f => f.Random.String(15))
        .RuleFor(u => u.StockQuantity, f => f.Random.Number(5,50))
        .RuleFor(u => u.UnitPrice, f => f.Random.Decimal(2,1000));

    public static UpdateProductCommand GenerateValidCommand()
    {
        return UpdateProductHandlerFaker.Generate();
    }
}