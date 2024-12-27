using Ambev.DeveloperEvaluation.Domain.Entities;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;

public static class CompanyTestData
{
    private static readonly Faker<Company> CompanyFaker = new Faker<Company>()
        .RuleFor(u => u.UserId, Guid.NewGuid)
        .RuleFor(u => u.Name, f => f.Name.Random.ToString())
        .RuleFor(u => u.UpdatedAt, () => DateTime.UtcNow);

    public static Company GenerateValidCompany()
    {
        return CompanyFaker.Generate();
    }

    public static string GenerateInvalidLongName()
    {
        return new Faker().Random.String(51);
    }
    public static string GenerateInvalidMinName()
    {
        return new Faker().Random.String(5);
    }
}