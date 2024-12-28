using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

public record GetCustomerQuery : IRequest<GetCustomerResult>
{
    public Guid Id { get; }
    
    public GetCustomerQuery(Guid id)
    {
        Id = id;
    }

    public GetCustomerQuery()
    {
        
    }
}
