using Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company.TestData;

public static class DeleteCompanyHandlerTestData
{
    public static DeleteCompanyCommand GenerateValidCommand()
    {
        return new DeleteCompanyCommand(Guid.NewGuid());
    }
}