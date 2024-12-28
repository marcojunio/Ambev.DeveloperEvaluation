using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Services.Discount;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class SaleItem : BaseEntity
{
    public Product Product { get; set; } = null!;

    public Guid ProductId { get; set; }
    public User User { get; set; } = null!;
    public Guid UserId { get; set; }
    public Sale Sale { get; set; } = null!;
    public Guid SaleId { get; set; }
    
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal? Discount { get; private set; }
    public decimal Total { get; private set; }

  
    
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

    public void ApplyDiscount()
    {
        var discountStrategy = DiscountStrategyFactory.GetStrategy(this);
        Discount = discountStrategy.CalculateDiscount(this);
        UnitPrice = Product.UnitPrice;
        
        Total = Quantity * UnitPrice * (1 - Discount.GetValueOrDefault(0));
    }
    
    public void CanSale()
    {
        if (Quantity > 20)
            throw new InvalidDomainOperation("Cannot sell more than 20 identical items.");
        
        if (Product.StockQuantity == 0)
            throw new ProductOutOfStockException($"Product {Product.Name} out of stock.");
        
        if (Product.StockQuantity < Quantity)
            throw new ProductOutOfStockException($"Quantity requested above available ({Product.StockQuantity})");
    }
}