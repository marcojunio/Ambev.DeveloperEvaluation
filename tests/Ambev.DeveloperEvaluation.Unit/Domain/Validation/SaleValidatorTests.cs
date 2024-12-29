using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation;

public class SaleValidatorTests
{
    private readonly SaleValidator _validator;

    public SaleValidatorTests()
    {
        _validator = new SaleValidator();
    }

    [Fact]
    public void Should_Have_Error_When_UserId_Is_Empty()
    {
        // Arrange
        var sale = new Sale
        {
            UserId = Guid.Empty,
            SellingCompanyId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.UserId)
            .WithErrorMessage("User required");
    }

    [Fact]
    public void Should_Not_Have_Error_When_UserId_Is_Valid()
    {
        // Arrange
        var sale = new Sale
        {
            UserId = Guid.NewGuid(),
            SellingCompanyId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.UserId);
    }

    [Fact]
    public void Should_Have_Error_When_SellingCompanyId_Is_Empty()
    {
        // Arrange
        var sale = new Sale
        {
            UserId = Guid.NewGuid(),
            SellingCompanyId = Guid.Empty,
            CustomerId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.SellingCompanyId)
            .WithErrorMessage("Selling company required");
    }

    [Fact]
    public void Should_Not_Have_Error_When_SellingCompanyId_Is_Valid()
    {
        // Arrange
        var sale = new Sale
        {
            UserId = Guid.NewGuid(),
            SellingCompanyId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.SellingCompanyId);
    }

    [Fact]
    public void Should_Have_Error_When_CustomerId_Is_Empty()
    {
        // Arrange
        var sale = new Sale
        {
            UserId = Guid.NewGuid(),
            SellingCompanyId = Guid.NewGuid(),
            CustomerId = Guid.Empty
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.CustomerId)
            .WithErrorMessage("Customer required");
    }

    [Fact]
    public void Should_Not_Have_Error_When_CustomerId_Is_Valid()
    {
        // Arrange
        var sale = new Sale
        {
            UserId = Guid.NewGuid(),
            SellingCompanyId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.CustomerId);
    }

    [Fact]
    public void Should_Not_Have_Error_When_All_Fields_Are_Valid()
    {
        // Arrange
        var sale = new Sale
        {
            UserId = Guid.NewGuid(),
            SellingCompanyId = Guid.NewGuid(),
            CustomerId = Guid.NewGuid()
        };

        // Act
        var result = _validator.TestValidate(sale);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
}