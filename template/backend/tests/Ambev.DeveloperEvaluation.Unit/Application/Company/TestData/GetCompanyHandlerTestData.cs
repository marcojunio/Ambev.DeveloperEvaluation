using Ambev.DeveloperEvaluation.Application.Companies.GetCompany;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company.TestData;

public static class GetCompanyHandlerTestData
{
    public static GetCompanyQuery GenerateValidCommand()
    {
        return new GetCompanyQuery(Guid.NewGuid());
    }
}