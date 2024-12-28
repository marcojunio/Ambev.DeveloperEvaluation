namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale;

public class DeleteSaleRequest
{
    public Guid Id { get; set; }

    public DeleteSaleRequest(Guid id)
    {
        Id = id;
    }
}