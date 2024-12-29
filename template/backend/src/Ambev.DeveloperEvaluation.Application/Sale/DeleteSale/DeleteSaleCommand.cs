using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.DeleteSale;

public record DeleteSaleCommand : IRequest<DeleteSaleResult>
{
    public Guid Id { get; }
    public Guid UserId { get; }
    
    public DeleteSaleCommand(Guid id,Guid userId)
    {
        Id = id;
        UserId = userId;
    }

    public DeleteSaleCommand()
    {
        
    }
}
