using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Services.Discount;

public interface IDiscountStrategy
{
    decimal CalculateDiscount(SaleItem item);
}