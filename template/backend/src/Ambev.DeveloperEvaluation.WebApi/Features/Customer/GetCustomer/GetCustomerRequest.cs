namespace Ambev.DeveloperEvaluation.WebApi.Features.Customer.GetCustomer;

public class GetCustomerRequest
{
    public Guid Id { get; set; }
    
    public GetCustomerRequest(Guid id)
    {
        Id = id;
    }
}