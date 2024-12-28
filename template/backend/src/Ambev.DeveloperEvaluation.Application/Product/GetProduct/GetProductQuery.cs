using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct;

public record GetProductQuery : IRequest<GetProductResult>
{
    public Guid Id { get; }
    
    public GetProductQuery(Guid id)
    {
        Id = id;
    }

    public GetProductQuery()
    {
        
    }
}
