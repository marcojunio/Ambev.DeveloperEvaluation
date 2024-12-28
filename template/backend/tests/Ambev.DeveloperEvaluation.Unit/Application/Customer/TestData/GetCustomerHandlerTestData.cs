using Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customer.TestData;

public static class GetCustomerHandlerTestData
{
    public static GetCustomerQuery GenerateValidCommand()
    {
        return new GetCustomerQuery(Guid.NewGuid());
    }
}