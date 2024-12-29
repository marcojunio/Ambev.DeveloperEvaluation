using Ambev.DeveloperEvaluation.Domain.Dtos;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetListSale;

public class GetListSaleResult
{
    public Guid Id { get; set; }
    public Guid SellingCompanyId { get; set; }
    public Guid CustomerId { get; set; }
    public string SaleNumber { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public bool IsCancelled { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public IEnumerable<SaleItemResultDto> SaleItems { get; set; } = new List<SaleItemResultDto>();
}