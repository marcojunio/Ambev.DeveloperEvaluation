using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct;

public record GetProductCommand : IRequest<GetProductResult>
{
    public Guid Id { get; }
    
    public GetProductCommand(Guid id)
    {
        Id = id;
    }

    public GetProductCommand()
    {
        
    }
}
