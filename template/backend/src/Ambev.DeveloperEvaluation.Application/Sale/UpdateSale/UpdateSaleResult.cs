using Ambev.DeveloperEvaluation.Domain.Dtos;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

public class UpdateSaleResult
{
    public Guid Id { get; set; }
    public Guid SellingCompanyId { get; set; }
    public Guid UserId { get; set; }
    public bool IsCancelled { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public IEnumerable<SaleItemResultDto> SaleItems { get; set; } = new List<SaleItemResultDto>();
}