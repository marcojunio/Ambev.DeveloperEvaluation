using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid sale data")]
    public void Given_ValidSaleItemData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var sale = SaleTestsData.GenerateValidSale();

        // Act
        var result = sale.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
    
    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// </summary>
    [Fact(DisplayName = "Should be success when add sale item list")]
    public void Should_be_sucess_when_add_sale_item_list()
    {
        // Arrange
        var sale = SaleTestsData.GenerateValidSale();
        var product = ProductTestData.GenerateValidProduct();
        
        // Act
        sale.AddItem(new SaleItem()
        {
            Id = Guid.NewGuid(),
            Product = product,
            Quantity = 10,
            UserId = Guid.NewGuid(),
            UnitPrice = 5,
        });

        // Assert
        Assert.Equal(490,product.StockQuantity);
    }
    
    /// <summary>
    /// Tests that validation passes when all sale properties are valid.
    /// </summary>
    [Fact(DisplayName = "Should be success when remove sale item list")]
    public void Should_be_sucess_when_remove_sale_item_list()
    {
        // Arrange
        var sale = SaleTestsData.GenerateValidSale();
        var product = ProductTestData.GenerateValidProduct();
        var saleItem = new SaleItem()
        {
            Id = Guid.NewGuid(),
            Product = product,
            Quantity = 10,
            UserId = Guid.NewGuid(),
            UnitPrice = 5,
        };
        sale.AddItem(saleItem);

        // Assert add
        Assert.Equal(3,sale.Items.Count);
        
        //Act
        sale.RemoveItem(saleItem);
        
        // Assert add
        Assert.Equal(2,sale.Items.Count);
    }
}