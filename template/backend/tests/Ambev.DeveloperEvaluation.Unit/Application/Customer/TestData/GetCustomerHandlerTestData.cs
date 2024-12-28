using Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customer.TestData;

public static class GetCustomerHandlerTestData
{
    public static GetCustomerCommand GenerateValidCommand()
    {
        return new GetCustomerCommand(Guid.NewGuid());
    }
}