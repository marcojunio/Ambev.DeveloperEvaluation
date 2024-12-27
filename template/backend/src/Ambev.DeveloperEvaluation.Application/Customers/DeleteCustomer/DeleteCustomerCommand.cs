using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;

public record DeleteCustomerCommand : IRequest<DeleteCustomerResult>
{
    public Guid Id { get; }
    
    public DeleteCustomerCommand(Guid id)
    {
        Id = id;
    }
}
