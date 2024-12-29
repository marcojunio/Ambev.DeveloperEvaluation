using Ambev.DeveloperEvaluation.Application.Product.CreateProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Product;

public class CreateProductHandlerTests
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateProductHandler _handler;

    public CreateProductHandlerTests()
    {
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateProductHandler(_productRepository, _mapper,Substitute.For<IMediator>());
    }

    [Fact(DisplayName = "Given valid product data When creating user Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();

        var product = new DeveloperEvaluation.Domain.Entities.Product()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            UserId = command.UserId,
            UnitPrice = 50
        };
        
        product.IncreaseStock(10);

        var result = new CreateProductResult()
        {
            Id = product.Id,
        };

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Product>(command).Returns(product);
        _mapper.Map<CreateProductResult>(product).Returns(result);

        _productRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Product>(), CancellationToken.None)
            .Returns(product);

        // When
        var createProductResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createProductResult.Should().NotBeNull();
        createProductResult.Id.Should().Be(product.Id);
        await _productRepository.Received(1).CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Product>(),
            CancellationToken.None);
    }

    [Fact(DisplayName = "Given invalid product data When creating user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateProductCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given valid command When handling Then maps command to user entity")]
    public async Task Handle_ValidRequest_MapsCommandToProduct()
    {
        // Given
        var command = CreateProductHandlerTestData.GenerateValidCommand();

        var product = new DeveloperEvaluation.Domain.Entities.Product()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            UserId = command.UserId,
            UnitPrice = 50
        };

         product.IncreaseStock(10);
         
        _mapper.Map<DeveloperEvaluation.Domain.Entities.Product>(command).Returns(product);

        _productRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Product>(), CancellationToken.None)
            .Returns(product);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<DeveloperEvaluation.Domain.Entities.Product>(Arg.Is<CreateProductCommand>(c =>
            c.Name == command.Name &&
            c.UnitPrice == command.UnitPrice &&
            c.StockQuantity == command.StockQuantity &&
            c.UserId == command.UserId));
    }
}