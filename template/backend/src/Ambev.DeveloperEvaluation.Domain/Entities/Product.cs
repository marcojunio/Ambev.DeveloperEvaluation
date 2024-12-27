using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Validation;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; } = 0;
    public Guid UserId { get; set; }
    public User User { get; set; }
    
    public int StockQuantity { get; private set; } = 0;
    
    public ValidationResultDetail Validate()
    {
        var validator = new ProductValidator();
        var result = validator.Validate(this);
        return new ValidationResultDetail
        {
            IsValid = result.IsValid,
            Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
        };
    }
    
    public void IncreaseStock(int quantity)
    {
        if (IsValidChangeStockQuantity(quantity))
            return;

        StockQuantity += quantity;
    }

    public void DecreaseStock(int quantity)
    {
        if (IsValidChangeStockQuantity(quantity))
            return;

        if(quantity > StockQuantity)
            return;
        
        StockQuantity -= quantity;
    }

    private bool IsValidChangeStockQuantity(int quantity) => quantity is 0 or < 0;
}