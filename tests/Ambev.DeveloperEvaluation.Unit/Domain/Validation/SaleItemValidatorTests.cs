using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class SaleItemValidatorTests
{
    private readonly SaleItemValidator _validator;

    public SaleItemValidatorTests()
    {
        _validator = new SaleItemValidator();
    }

    [Fact]
    public void Should_Have_Error_When_UnitPrice_Is_Zero()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            UnitPrice = 0m,
            Quantity = 10,
            UserId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UnitPrice)
            .WithErrorMessage("Unit price must be greater than zero");
    }

    [Fact]
    public void Should_Have_Error_When_UnitPrice_Is_Negative()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            UnitPrice = -10m,
            Quantity = 10,
            UserId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UnitPrice)
            .WithErrorMessage("Unit price must be greater than zero");
    }

    [Fact]
    public void Should_Not_Have_Error_When_UnitPrice_Is_Greater_Than_Zero()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            UnitPrice = 10m,
            Quantity = 10,
            UserId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.UnitPrice);
    }

    [Fact]
    public void Should_Have_Error_When_Quantity_Is_Zero()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            UnitPrice = 10m,
            Quantity = 0,
            UserId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Quantity)
            .WithErrorMessage("Quantity be greater than zero");
    }

    [Fact]
    public void Should_Have_Error_When_Quantity_Is_Negative()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            UnitPrice = 10m,
            Quantity = -1,
            UserId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Quantity)
            .WithErrorMessage("Quantity be greater than zero");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Quantity_Is_Greater_Than_Zero()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            UnitPrice = 10m,
            Quantity = 10,
            UserId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Quantity);
    }

    [Fact]
    public void Should_Have_Error_When_UserId_Is_Empty()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            UnitPrice = 10m,
            Quantity = 10,
            UserId = Guid.Empty
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId)
            .WithErrorMessage("User required");
    }

    [Fact]
    public void Should_Not_Have_Error_When_UserId_Is_Valid()
    {
        // Arrange
        var saleItem = new SaleItem
        {
            UnitPrice = 10m,
            Quantity = 10,
            UserId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(saleItem);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.UserId);
    }
}