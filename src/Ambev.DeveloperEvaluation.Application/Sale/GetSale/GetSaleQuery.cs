using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale;

public record GetSaleQuery : IRequest<GetSaleResult>
{
    public Guid Id { get; }
    
    public GetSaleQuery(Guid id)
    {
        Id = id;
    }

    public GetSaleQuery()
    {
        
    }
}
