using Ambev.DeveloperEvaluation.Application.Product.GetProduct;
using Ambev.DeveloperEvaluation.Common.Cache;
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

public class GetProductHandlerTests
{
    private readonly  IMapper _mapper;
    private readonly  IProductRepository _productRepository;
    private readonly  GetProductHandler _getProductHandler;

    public GetProductHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _productRepository = Substitute.For<IProductRepository>();
        _getProductHandler = new GetProductHandler(_productRepository, _mapper,Substitute.For<ICacheService>());
    }
    
    
    [Fact(DisplayName = "Should successfully get a product")]
    public async Task Should_successfully_get_a_product()
    {
        //fact
        var command = GetProductHandlerTestData.GenerateValidCommand();

        var product = new DeveloperEvaluation.Domain.Entities.Product
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            UnitPrice = 25
        };
        
        product.IncreaseStock(40);
        
        var result = new GetProductResult()
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            UnitPrice = 25,
            StockQuantity = 40
        };

        _mapper.Map<GetProductResult>(Arg.Any<DeveloperEvaluation.Domain.Entities.Product>()).Returns(result);
        
        _productRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Is(CancellationToken.None)).Returns(product);

        // When
        var resultHandler = await _getProductHandler.Handle(command, CancellationToken.None);
        
        //Then
        Assert.Equal(result.Id,resultHandler.Id);
        Assert.Equal(result.Name,resultHandler.Name);
    }
    
    [Fact(DisplayName = "Should insuccessfully get a product")]
    public async Task Should_insuccessfully_get_a_product()
    {
        //fact
        var command = new GetProductQuery();

        // When
        var act = () =>  _getProductHandler.Handle(command, CancellationToken.None);
        
        //Then
        await act.Should().ThrowAsync<ValidationException>();
    }
    
    [Fact(DisplayName = "Shouldn't find a product")]
    public async Task Shouldnt_find_a_product()
    {
        //fact
        var command = new GetProductQuery(Guid.NewGuid());
        
        // When
        var act = async () => await _getProductHandler.Handle(command, CancellationToken.None);
        
        //Then
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Product with ID {command.Id} not found");
    }
}