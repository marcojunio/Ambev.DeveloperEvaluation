using Ambev.DeveloperEvaluation.Application.Product.UpdateProduct;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product;

public class UpdateProductHandlerTests
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly UpdateProductHandler _handler;

    public UpdateProductHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _productRepository = Substitute.For<IProductRepository>();
        _handler = new UpdateProductHandler(_productRepository, _mapper,Substitute.For<IMediator>());
    }

    [Fact(DisplayName = "Should update with success a product")]
    public async Task Should_update_with_success_a_product()
    {
        //Fact
        var command = UpdateProductHandlerTestData.GenerateValidCommand();

        var date = DateTime.UtcNow;

        var product = new DeveloperEvaluation.Domain.Entities.Product()
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            UnitPrice = 25
        };
        
        product.IncreaseStock(10);

        var result = new UpdateProductResult()
        {
            Id = Guid.NewGuid(),
            CreatedAt = date,
            Name = "Test update",
            UnitPrice = 30,
            StockQuantity = 40
        };

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Product>(command).Returns(product);
        _mapper.Map<UpdateProductResult>(Arg.Any<DeveloperEvaluation.Domain.Entities.Product>()).Returns(result);

        _productRepository
            .UpdateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Product>(), CancellationToken.None).Returns(
                new DeveloperEvaluation.Domain.Entities.Product()
                {
                    Id = result.Id,
                    Name = result.Name,
                    UpdatedAt = result.UpdatedAt
                });

        //When
        var resultHandler = await _handler.Handle(command, CancellationToken.None);

        //Then
        Assert.NotEqual(product.Name, resultHandler.Name);
        Assert.NotEqual(product.UnitPrice, resultHandler.UnitPrice);
        Assert.NotEqual(product.StockQuantity, resultHandler.StockQuantity);
        product.UpdatedAt.Should().NotBeNull();
    }

    [Fact(DisplayName = "Should update with exception a product")]
    public async Task Should_update_with_failure_a_product()
    {
        //Fact
        var command = new UpdateProductCommand();

        //When
        var act = () => _handler.Handle(command, CancellationToken.None);

        //Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Should update with exception a product because name exist")]
    public async Task Should_update_with_failure_a_product_because_name_exist()
    {
        // Fact
        var command = UpdateProductHandlerTestData.GenerateValidCommand();

        var existingProduct = new DeveloperEvaluation.Domain.Entities.Product
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            UnitPrice = 25
        };
        
        existingProduct.IncreaseStock(10);

        _productRepository
            .GetByNameAsync(command.UserId, command.Name, CancellationToken.None)
            .Returns(existingProduct);

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Product>(command).Returns(existingProduct);

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidDomainOperation>()
            .WithMessage($"Product with name {command.Name} already exists");
    }
}