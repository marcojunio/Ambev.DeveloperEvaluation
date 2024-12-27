using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class CompanyTests
{
    /// <summary>
    /// Tests that validation passes when all company properties are valid.
    /// </summary>
    [Fact(DisplayName = "Validation should pass for valid company data")]
    public void Given_ValidCompanyData_When_Validated_Then_ShouldReturnValid()
    {
        // Arrange
        var costumer = CompanyTestData.GenerateValidCompany();

        // Act
        var result = costumer.Validate();

        // Assert
        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    /// <summary>
    /// Tests that validation fails when company properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid long name for company")]
    public void Given_InvalidCompanyData_When_Validated_Then_ShouldReturnInvalid_LongName()
    {
        // Arrange
        var costumer = new Company()
        {
            Name = CompanyTestData.GenerateInvalidLongName()
        };

        // Act
        var result = costumer.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
    
    /// <summary>
    /// Tests that validation fails when company properties are invalid.
    /// </summary>
    [Fact(DisplayName = "Validation should fail for invalid min name for company")]
    public void Given_InvalidCompanyData_When_Validated_Then_ShouldReturnInvalid_MinName()
    {
        // Arrange
        var costumer = new Company()
        {
            Name = CompanyTestData.GenerateInvalidMinName()
        };

        // Act
        var result = costumer.Validate();

        // Assert
        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
    }
}