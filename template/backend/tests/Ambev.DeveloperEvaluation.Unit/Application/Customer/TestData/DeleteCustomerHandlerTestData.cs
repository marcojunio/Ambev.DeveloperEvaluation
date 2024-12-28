using Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customer.TestData;

public static class DeleteCustomerHandlerTestData
{
    public static DeleteCustomerCommand GenerateValidCommand()
    {
        return new DeleteCustomerCommand(Guid.NewGuid());
    }
}