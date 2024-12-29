using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Sale : BaseEntity
{
    public Sale() => SaleNumber = Guid.NewGuid().ToString("N")[..16].ToUpper().Replace("-", "");

    public string SaleNumber { get; set; }

    public Guid CustomerId { get; set; }
    public Guid SellingCompanyId { get; set; }
    public User User { get; set; } = null!;
    public Guid UserId { get; set; }

    public bool IsCancelled { get; set; }


    private readonly List<SaleItem> _items = new();
    public IReadOnlyCollection<SaleItem> Items => _items;
    public decimal Amount { get; private set; }

    public void AddItem(SaleItem saleItem)
    {
        _items.Add(saleItem);
    }

    public void RemoveItem(SaleItem item)
    {
        _items.Remove(item);
    }


    public void VerifyItemsAndApplyCalculate(bool updating = false)
    {
        foreach (var item in _items)
        {
            item.CanSale();

            item.ApplyDiscount();

            item.UserId = UserId;

            if (updating)
                item.UpdateDate();
        }

        CalculateAmount();
    }


    public void CancelSale()
    {
        IsCancelled = true;

        foreach (var item in _items)
            item.CancelSale();
        
        CalculateAmount();
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

    private void CalculateAmount()
    {
        Amount = _items.Sum(item => item.Total);
    }
}