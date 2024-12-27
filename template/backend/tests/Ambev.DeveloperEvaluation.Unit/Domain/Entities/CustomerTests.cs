using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class CustomerTests
{
    /// <summary>
    /// Tests that validation passes when all customer properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid customer data")]
    public void Given_ValidCustomerData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var customer = CustomerTestData.GenerateValidCustomer();

        // Act
        var result = customer.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when customer properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid long name for customer")]
    public void Given_InvalidCustomerData_When_Validated_Then_ShouldReturnInvalid_LongName()
    {
        // Arrange
        
        var customerValid = CustomerTestData.GenerateValidCustomer();
        
        var customer = new Customer()
        {
            Age = customerValid.Age,
            Name = CustomerTestData.GenerateInvalidLongName(),
            UserId = Guid.NewGuid()
        };

        // Act
        var result = customer.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Single(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when customer properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid min name for customer")]
    public void Given_InvalidCustomerData_When_Validated_Then_ShouldReturnInvalid_MinName()
    {
        // Arrange
        
        var customerValid = CustomerTestData.GenerateValidCustomer();
        
        var customer = new Customer()
        {
            Age = customerValid.Age,
            Name = CustomerTestData.GenerateInvalidMinName(),
            UserId = Guid.NewGuid()
        };

        // Act
        var result = customer.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Single(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when customer properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid long age for customer")]
    public void Given_InvalidCustomerData_When_Validated_Then_ShouldReturnInvalid_LongAge()
    {
        // Arrange
        
        var customerValid = CustomerTestData.GenerateValidCustomer();
        
        var customer = new Customer()
        {
            Name = customerValid.Name,
            Age = CustomerTestData.GenerateInvalidLongAge(),
            UserId = Guid.NewGuid()
        };

        // Act
        var result = customer.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Single(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when customer properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid min age for customer")]
    public void Given_InvalidCustomerData_When_Validated_Then_ShouldReturnInvalid_MinAge()
    {
        // Arrange
        var customerValid = CustomerTestData.GenerateValidCustomer();
        
        var customer = new Customer()
        {
            Name = customerValid.Name,
            Age = CustomerTestData.GenerateInvalidMinAge(),
            UserId = Guid.NewGuid()
        };

        // Act
        var result = customer.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Single(result.Errors);
    }
}