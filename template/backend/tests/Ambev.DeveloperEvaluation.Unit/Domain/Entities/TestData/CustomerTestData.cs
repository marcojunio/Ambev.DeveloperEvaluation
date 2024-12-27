using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class CustomerTestData
{
    private static readonly Faker<Customer> CustomerFaker = new Faker<Customer>()
        .RuleFor(u => u.UserId, Guid.NewGuid)
        .RuleFor(u => u.Name, f => f.Name.Random.ToString())
        .RuleFor(u => u.Age, f => f.Random.Number(1, 50))
        .RuleFor(u => u.UpdatedAt, () => DateTime.UtcNow);

    public static Customer GenerateValidCustomer()
    {
        return CustomerFaker.Generate();
    }

    public static string GenerateInvalidLongName()
    {
        return new Faker().Random.String(51);
    }

    public static string GenerateInvalidMinName()
    {
        return new Faker().Random.String(5);
    }

    public static int GenerateInvalidLongAge()
    {
        return new Faker().Random.Number(151,200);
    }

    public static int GenerateInvalidMinAge()
    {
        return -50;
    }
}