using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleItemTests
{
    /// <summary>
    /// Tests that validation passes when all sale item properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid sale item data")]
    public void Given_ValidSaleItemData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();

        // Act
        var result = saleItem.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    /// <summary>
    /// Tests that validation passes when all sale item properties are valid.
    /// </summary>
    [Fact(DisplayName = "Should be fail with exception product out of stock")]
    public void Should_be_fail_with_exception_product_out_of_stock()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();

        var product = new Product()
        {
            Name = "Teste",
            UnitPrice = 1,
        };
        
        product.IncreaseStock(1);
        saleItem.Product = product;
        
        // Assert
        Assert.Throws<ProductOutOfStockException>(() => saleItem.CanSale());
    }
    
    /// <summary>
    /// Tests that validation passes when all sale item properties are valid.
    /// </summary>
    [Fact(DisplayName = "Should be success in calculate discount for product")]
    public void Should_be_success_in_calculate_discount_for_product()
    {
        // Arrange
        var saleItem = SaleItemTestData.GenerateValidSaleItem();
        saleItem.Quantity = 1;
        saleItem.UnitPrice = 100;
        
        var product = new Product()
        {
            Name = "Teste",
            UnitPrice = 100,
        };
        
        product.IncreaseStock(1);
    
        saleItem.Product = product;
        
        //Act
        saleItem.ApplyDiscount();
        
        // Assert
        Assert.Equal(85,saleItem.Total);
    }
}