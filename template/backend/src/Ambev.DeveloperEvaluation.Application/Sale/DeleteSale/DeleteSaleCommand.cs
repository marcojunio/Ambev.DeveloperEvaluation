using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.DeleteSale;

public record DeleteSaleCommand : IRequest<DeleteSaleResult>
{
    public Guid Id { get; }
    
    public DeleteSaleCommand(Guid id)
    {
        Id = id;
    }
}
