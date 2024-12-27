using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public Sale() => SaleNumber = Guid.NewGuid().ToString()[..16].ToUpper().Replace("-", "");
    
    public string SaleNumber { get;}
    
    public Customer Customer { get; set; } = new();
    public Guid CustomerId { get; set; }
    public Company SellingCompany { get; set; } = new();
    public Guid SellingCompanyId { get; set; }
    public User User { get; set; } = new();
    public Guid UserId { get; set; }
    
    public bool IsCancelled { get; set; }

    public decimal Amount { get; set; }

    private readonly List<SaleItem> _items = new();
    public IReadOnlyCollection<SaleItem> Items => _items;

    public void AddItem(SaleItem saleItem)
    {
        saleItem.CanSale();

        saleItem.Product.DecreaseStock(saleItem.Quantity);

        _items.Add(saleItem);

        Amount = _items.Sum(item => item.Total);
    }

    public void RemoveItem(SaleItem item)
    {
        item.Product.IncreaseStock(item.Quantity);

        _items.Remove(item);
        
        Amount = _items.Sum(i => item.Total);
    }

    public void CancelSale()
    {
        IsCancelled = true;
    }
    
    public ValidationResultDetail Validate()
    {
        var validator = new SaleValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
}