using Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customer.TestData;

public static class UpdateCustomerHandlerTestData
{
    private static readonly Faker<UpdateCustomerCommand> UpdateCustomerHandlerFaker = new Faker<UpdateCustomerCommand>()
        .RuleFor(u => u.Id, Guid.NewGuid)
        .RuleFor(u => u.UserId, Guid.NewGuid)
        .RuleFor(u => u.Name, f => f.Random.String(15))
        .RuleFor(u => u.Age, f => f.Random.Number(5,50));

    public static UpdateCustomerCommand GenerateValidCommand()
    {
        return UpdateCustomerHandlerFaker.Generate();
    }
}