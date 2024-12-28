using Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company.TestData;

public static class UpdateCompanyHandlerTestData
{
    private static readonly Faker<UpdateCompanyCommand> UpdateComapanyHandlerFaker = new Faker<UpdateCompanyCommand>()
        .RuleFor(u => u.Id, Guid.NewGuid)
        .RuleFor(u => u.UserId, Guid.NewGuid)
        .RuleFor(u => u.Name, f => f.Internet.UserName());

    public static UpdateCompanyCommand GenerateValidCommand()
    {
        return UpdateComapanyHandlerFaker.Generate();
    }
}