namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

public class DeleteSaleRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }

    public DeleteSaleRequest(Guid id,Guid userId)
    {
        Id = id;
        UserId = userId;
    }

    public DeleteSaleRequest()
    {
        
    }
}