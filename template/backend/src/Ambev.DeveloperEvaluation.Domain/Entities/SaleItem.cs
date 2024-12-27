using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Product Product { get; set; } = new();

    public Guid ProductId { get; set; }
    public User User { get; set; } = new();
    public Guid UserId { get; set; }
    public Sale Sale { get; set; } = new();
    public Guid SaleId { get; set; }
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? Discount { get; private set; }
    public decimal Total { get; private set; }
  

    public void ApplyDiscount(decimal discountPercentage)
    {
        if(discountPercentage is 0 or > 1)
            return;
        
        Discount = discountPercentage;
        Total = Quantity * UnitPrice * (1 - Discount.GetValueOrDefault(0));
    }
    
    public ValidationResultDetail Validate()
    {
        var validator = new SaleItemValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }

    public void CanSale()
    {
        if (Product.StockQuantity == 0)
            throw new ProductOutOfStockException($"Product {Product.Name} out of stock.");
        
        if (Product.StockQuantity < Quantity)
            throw new ProductOutOfStockException($"Quantity requested above available ({Product.StockQuantity})");
    }
}