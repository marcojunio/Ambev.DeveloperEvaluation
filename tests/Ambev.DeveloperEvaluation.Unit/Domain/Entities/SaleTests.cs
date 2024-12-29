using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    [Fact]
    public void Sale_Should_Generate_SaleNumber_When_Created()
    {
        // Arrange
        var sale = new Sale();

        // Act
        var saleNumber = sale.SaleNumber;

        // Assert
        saleNumber.Should().NotBeNullOrEmpty();
        saleNumber.Length.Should().Be(16);
    }

    [Fact]
    public void Sale_Should_Add_Item_Correctly()
    {
        // Arrange
        var sale = new Sale();
        var saleItem = Substitute.For<SaleItem>();
        saleItem.Quantity = 2;
        saleItem.UnitPrice = 10m;

        // Act
        sale.AddItem(saleItem);
        sale.VerifyItemsAndApplyCalculate();

        // Assert
        sale.Items.Should().Contain(saleItem);
        sale.Amount.Should().Be(20m);
    }

    [Fact]
    public void Sale_Should_Remove_Item_Correctly()
    {
        // Arrange
        var sale = new Sale();
        var saleItem = Substitute.For<SaleItem>();
        saleItem.Quantity = 2;
        saleItem.UnitPrice = 10m;

        sale.AddItem(saleItem);

        // Act
        sale.RemoveItem(saleItem);

        // Assert
        sale.Items.Should().NotContain(saleItem);
        sale.Amount.Should().Be(0m);
    }

    [Fact]
    public void Sale_Should_Throw_When_Item_Quantity_Exceeds_Limit()
    {
        // Arrange
        var sale = new Sale();
        var saleItem = Substitute.For<SaleItem>();
        saleItem.Quantity = 21;
        saleItem.UnitPrice = 10m;

        // Act
        Action act = () => saleItem.CanSale();

        // Assert
        act.Should().Throw<InvalidDomainOperation>().WithMessage("Cannot sell more than 20 identical items.");
    }

    [Fact]
    public void Sale_Should_Calculate_Amount_Correctly_After_Item_Added()
    {
        // Arrange
        var sale = new Sale();
        var saleItem1 = Substitute.For<SaleItem>();
        saleItem1.Quantity = 2;
        saleItem1.UnitPrice = 10m;

        var saleItem2 = Substitute.For<SaleItem>();
        saleItem2.Quantity = 3;
        saleItem2.UnitPrice = 5m;

        sale.AddItem(saleItem1);
        sale.AddItem(saleItem2);

        // Act
        sale.VerifyItemsAndApplyCalculate();

        // Assert
        sale.Amount.Should().Be(35m);
    }

    [Fact]
    public void Sale_Should_Cancel_Sale_Correctly()
    {
        // Arrange
        var sale = new Sale();
        var saleItem = Substitute.For<SaleItem>();
        saleItem.Quantity = 2;
        saleItem.UnitPrice = 10m;
        sale.AddItem(saleItem);

        // Act
        sale.VerifyItemsAndApplyCalculate();
        sale.CancelSale();

        // Assert
        sale.IsCancelled.Should().BeTrue();
        sale.Items.Should().Contain(saleItem);
        sale.Amount.Should().Be(0m);
    }

    [Fact]
    public void Sale_Should_Validate_Correctly_When_Valid()
    {
        // Arrange
        var sale = new Sale
        {
            CustomerId = Guid.NewGuid(),
            SellingCompanyId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var saleItem = Substitute.For<SaleItem>();
        saleItem.Quantity = 2;
        saleItem.UnitPrice = 10m;

        sale.AddItem(saleItem);

        // Act
        var validationResult = sale.Validate();

        // Assert
        validationResult.IsValid.Should().BeTrue();
    }

    [Fact]
    public void Sale_Should_Validate_Fail_When_Missing_CustomerId()
    {
        // Arrange
        var sale = new Sale
        {
            SellingCompanyId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var saleItem = Substitute.For<SaleItem>();
        saleItem.Quantity = 2;
        saleItem.UnitPrice = 10m;

        sale.AddItem(saleItem);

        // Act
        var validationResult = sale.Validate();

        // Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should()
            .Contain(e => e.Detail == "Customer required" && e.Error == "NotEmptyValidator");
    }

    [Fact]
    public void Sale_Should_Validate_Fail_When_Missing_SellingCompanyId()
    {
        // Arrange
        var sale = new Sale
        {
            CustomerId = Guid.NewGuid(),
            UserId = Guid.NewGuid()
        };

        var saleItem = Substitute.For<SaleItem>();
        saleItem.Quantity = 2;
        saleItem.UnitPrice = 10m;

        sale.AddItem(saleItem);

        // Act
        var validationResult = sale.Validate();

        // Assert
        validationResult.IsValid.Should().BeFalse();
        validationResult.Errors.Should()
            .Contain(e => e.Detail == "Selling company required" && e.Error == "NotEmptyValidator");
    }
}