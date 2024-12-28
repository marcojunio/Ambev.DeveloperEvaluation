using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customer.TestData;

public static class CreateCustomerHandlerTestData
{
    private static readonly Faker<CreateCustomerCommand> CreateCustomerHandlerFaker = new Faker<CreateCustomerCommand>()
        .RuleFor(u => u.UserId, f => Guid.NewGuid())
        .RuleFor(u => u.Name, f => f.Random.String(15))
        .RuleFor(u => u.Age, f => f.Random.Number(5,50));

    public static CreateCustomerCommand GenerateValidCommand()
    {
        return CreateCustomerHandlerFaker.Generate();
    }
}