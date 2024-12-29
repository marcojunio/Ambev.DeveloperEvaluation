using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class SaleItemUpdateDtoValidatorTests
    {
        private readonly SaleItemUpdateDtoValidator _validator;

        public SaleItemUpdateDtoValidatorTests()
        {
            _validator = new SaleItemUpdateDtoValidator();
        }

        [Fact]
        public void Should_Have_Error_When_Quantity_Is_Null()
        {
            // Arrange
            var saleItemUpdateDto = new SaleItemUpdateDto(Guid.NewGuid(), Guid.NewGuid(), 0, 10.0m);

            // Act
            var result = _validator.TestValidate(saleItemUpdateDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Quantity)
                .WithErrorMessage("Quantity be greater than zero");
        }

        [Fact]
        public void Should_Have_Error_When_Quantity_Is_Less_Than_One()
        {
            // Arrange
            var saleItemUpdateDto = new SaleItemUpdateDto(Guid.NewGuid(), Guid.NewGuid(), 0, 10.0m);

            // Act
            var result = _validator.TestValidate(saleItemUpdateDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Quantity)
                .WithErrorMessage("Quantity be greater than zero");
        }

        [Fact]
        public void Should_Not_Have_Error_When_Quantity_Is_Greater_Than_Zero()
        {
            // Arrange
            var saleItemUpdateDto = new SaleItemUpdateDto(Guid.NewGuid(), Guid.NewGuid(), 5, 10.0m);

            // Act
            var result = _validator.TestValidate(saleItemUpdateDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.Quantity);
        }

        [Fact]
        public void Should_Have_Error_When_UnitPrice_Is_Null()
        {
            // Arrange
            var saleItemUpdateDto = new SaleItemUpdateDto(Guid.NewGuid(), Guid.NewGuid(), 5, 0m);

            // Act
            var result = _validator.TestValidate(saleItemUpdateDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UnitPrice)
                .WithErrorMessage("Unit price be greater than zero");
        }

        [Fact]
        public void Should_Have_Error_When_UnitPrice_Is_Less_Than_One()
        {
            // Arrange
            var saleItemUpdateDto = new SaleItemUpdateDto(Guid.NewGuid(), Guid.NewGuid(), 5, -10m);

            // Act
            var result = _validator.TestValidate(saleItemUpdateDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.UnitPrice)
                .WithErrorMessage("Unit price be greater than zero");
        }

        [Fact]
        public void Should_Not_Have_Error_When_UnitPrice_Is_Greater_Than_Zero()
        {
            // Arrange
            var saleItemUpdateDto = new SaleItemUpdateDto(Guid.NewGuid(), Guid.NewGuid(), 5, 10.0m);

            // Act
            var result = _validator.TestValidate(saleItemUpdateDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.UnitPrice);
        }

        [Fact]
        public void Should_Have_Error_When_ProductId_Is_Empty()
        {
            // Arrange
            var saleItemUpdateDto = new SaleItemUpdateDto(Guid.NewGuid(), Guid.Empty, 5, 10.0m);

            // Act
            var result = _validator.TestValidate(saleItemUpdateDto);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.ProductId)
                .WithErrorMessage("Product is required");
        }

        [Fact]
        public void Should_Not_Have_Error_When_ProductId_Is_Valid()
        {
            // Arrange
            var saleItemUpdateDto = new SaleItemUpdateDto(Guid.NewGuid(), Guid.NewGuid(), 5, 10.0m);

            // Act
            var result = _validator.TestValidate(saleItemUpdateDto);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.ProductId);
        }
    }
}
