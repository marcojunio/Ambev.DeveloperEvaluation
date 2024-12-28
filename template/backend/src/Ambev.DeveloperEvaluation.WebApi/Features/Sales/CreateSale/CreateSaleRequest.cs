using Ambev.DeveloperEvaluation.Domain.Dtos;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;

public class CreateSaleRequest
{
    public IEnumerable<SaleItemDto> SaleItems { get; set; } = new List<SaleItemDto>();
    public Guid CustomerId { get; set; }
    public Guid SellingCompanyId { get; set; }
}