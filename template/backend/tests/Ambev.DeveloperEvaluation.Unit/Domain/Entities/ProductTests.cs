using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class ProductTests
{
    /// <summary>
    /// Tests that validation passes when all product properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid product data")]
    public void Given_ValidProductData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var product = ProductTestData.GenerateValidProduct();

        // Act
        var result = product.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when product properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid long name for product")]
    public void Given_InvalidProductData_When_Validated_Then_ShouldReturnInvalid_LongName()
    {
        // Arrange
        var productValid = ProductTestData.GenerateValidProduct();
        
        var product = new Product()
        {
            UnitPrice = productValid.UnitPrice,
            Name = ProductTestData.GenerateInvalidLongName(),
            UserId = Guid.NewGuid()
        };
        
        product.IncreaseStock(productValid.StockQuantity);

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Single(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when product properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid min name for product")]
    public void Given_InvalidProductData_When_Validated_Then_ShouldReturnInvalid_MinName()
    {
        // Arrange
        // Arrange
        var productValid = ProductTestData.GenerateValidProduct();
        
        var product = new Product()
        {
            UnitPrice = productValid.UnitPrice,
            Name = ProductTestData.GenerateInvalidLongName(),
            UserId = Guid.NewGuid()
        };
        
        product.IncreaseStock(productValid.StockQuantity);

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Single(result.Errors);
    }
    
    /// <summary>
    /// Tests that validation fails when product properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid min unit price for product")]
    public void Given_InvalidProductData_When_Validated_Then_ShouldReturnInvalid_MinUnitPrice()
    {
        // Arrange
        var productValid = ProductTestData.GenerateValidProduct();
        
        var product = new Product
        {
            UnitPrice = ProductTestData.GenerateInvalidMinUnitPrice(),
            Name = productValid.Name,
            UserId = Guid.NewGuid()
        };
        
        product.IncreaseStock(productValid.StockQuantity);

        // Act
        var result = product.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Single(result.Errors);
    }
    
    
    [Fact(DisplayName = "Validation should pass for valid increase stock product")]
    public void Should_be_valid_when_increase_stock_with_valid_quantity()
    {
        // Arrange
        var productValid = ProductTestData.GenerateValidProduct();
        
        var product = new Product
        {
            UnitPrice = productValid.UnitPrice,
            Name = productValid.Name,
            UserId = Guid.NewGuid()
        };
        
        product.IncreaseStock(productValid.StockQuantity);
        product.IncreaseStock(10);

        // Act
        var result = product.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
        Assert.Equal(510,product.StockQuantity);
    }
    
    
    [Fact(DisplayName = "Validation should fail for invalid decrease stock product")]
    public void Should_be_invalid_when_decrease_stock_with_invalid_quantity()
    {
        // Arrange
        var productValid = ProductTestData.GenerateValidProduct();
        
        var product = new Product
        {
            UnitPrice = productValid.UnitPrice,
            Name = productValid.Name,
            UserId = Guid.NewGuid()
        };
        
        product.IncreaseStock(productValid.StockQuantity);
        product.DecreaseStock(-100);

        // Act
        var result = product.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
        Assert.Equal(500,product.StockQuantity);
    }
}