namespace Ambev.DeveloperEvaluation.WebApi.Features.Customer.DeleteCustomer;

public class DeleteCustomerRequest
{
    public Guid Id { get; set; }

    public DeleteCustomerRequest(Guid id)
    {
        Id = id;
    }
}