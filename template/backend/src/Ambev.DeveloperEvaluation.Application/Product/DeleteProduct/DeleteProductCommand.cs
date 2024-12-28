using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.DeleteProduct;

public record DeleteProductCommand : IRequest<DeleteProductResult>
{
    public Guid Id { get; }
    
    public DeleteProductCommand(Guid id)
    {
        Id = id;
    }
}
