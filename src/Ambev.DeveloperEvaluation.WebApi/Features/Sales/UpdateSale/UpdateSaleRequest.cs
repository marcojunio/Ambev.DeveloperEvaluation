using Ambev.DeveloperEvaluation.Domain.Dtos;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;

public class UpdateSaleRequest
{
    public Guid Id { get; set; }
    public Guid SellingCompanyId { get; set; }
    public Guid CustomerId { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public IEnumerable<SaleItemUpdateDto> SaleItems { get; set; } = new List<SaleItemUpdateDto>();
}