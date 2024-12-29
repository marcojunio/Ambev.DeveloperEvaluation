using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale;

public record CancelSaleCommand : IRequest<CancelSaleResult>
{
    public Guid Id { get; }
    
    public CancelSaleCommand(Guid id)
    {
        Id = id;
    }

    public CancelSaleCommand()
    {
        
    }
}
