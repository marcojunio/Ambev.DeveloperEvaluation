using Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company.TestData;

public static class CreateCompanyHandlerTestData
{
    private static readonly Faker<CreateCompanyCommand> CreateComapanyHandlerFaker = new Faker<CreateCompanyCommand>()
        .RuleFor(u => u.UserId, f => Guid.NewGuid())
        .RuleFor(u => u.Name, f => f.Internet.UserName());

    public static CreateCompanyCommand GenerateValidCommand()
    {
        return CreateComapanyHandlerFaker.Generate();
    }
}