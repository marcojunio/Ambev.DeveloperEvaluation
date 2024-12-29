using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class SaleItemDtoValidatorTests
{
    private readonly SaleItemDtoValidator _validator;

    public SaleItemDtoValidatorTests()
    {
        _validator = new SaleItemDtoValidator();
    }

    [Fact]
    public void Should_Have_Error_When_Quantity_Is_Null()
    {
        // Arrange
        var saleItemDto = new SaleItemDto(Guid.NewGuid(), 0, 10.0m); // Quantity can't be null as it's int, so use 0

        // Act
        var result = _validator.TestValidate(saleItemDto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Quantity)
            .WithErrorMessage("Quantity be greater than zero");
    }

    [Fact]
    public void Should_Have_Error_When_Quantity_Is_Less_Than_One()
    {
        // Arrange
        var saleItemDto = new SaleItemDto(Guid.NewGuid(), 0, 10.0m);

        // Act
        var result = _validator.TestValidate(saleItemDto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.Quantity)
            .WithErrorMessage("Quantity be greater than zero");
    }

    [Fact]
    public void Should_Not_Have_Error_When_Quantity_Is_Greater_Than_Zero()
    {
        // Arrange
        var saleItemDto = new SaleItemDto(Guid.NewGuid(), 5, 10.0m);

        // Act
        var result = _validator.TestValidate(saleItemDto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.Quantity);
    }

    [Fact]
    public void Should_Have_Error_When_ProductId_Is_Empty()
    {
        // Arrange
        var saleItemDto = new SaleItemDto(Guid.Empty, 5, 10.0m);

        // Act
        var result = _validator.TestValidate(saleItemDto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.ProductId)
            .WithErrorMessage("Product is required");
    }

    [Fact]
    public void Should_Not_Have_Error_When_ProductId_Is_Valid()
    {
        // Arrange
        var saleItemDto = new SaleItemDto(Guid.NewGuid(), 5, 10.0m);

        // Act
        var result = _validator.TestValidate(saleItemDto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.ProductId);
    }

    [Fact]
    public void Should_Have_Error_When_UnitPrice_Is_Zero()
    {
        // Arrange
        var saleItemDto = new SaleItemDto(Guid.NewGuid(), 5, 0m);

        // Act
        var result = _validator.TestValidate(saleItemDto);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UnitPrice)
            .WithErrorMessage("Unit price should be greater than zero");
    }

    [Fact]
    public void Should_Not_Have_Error_When_UnitPrice_Is_Greater_Than_Zero()
    {
        // Arrange
        var saleItemDto = new SaleItemDto(Guid.NewGuid(), 5, 10.0m);

        // Act
        var result = _validator.TestValidate(saleItemDto);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.UnitPrice);
    }
}