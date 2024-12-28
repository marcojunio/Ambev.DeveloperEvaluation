using Ambev.DeveloperEvaluation.Application.Product.DeleteProduct;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Product.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customer;

public class DeleteProductHandleTests
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _productRepository;
    private readonly DeleteProductHandler _deleteProductHandler;

    public DeleteProductHandleTests()
    {
        _mapper = Substitute.For<IMapper>();
        _productRepository = Substitute.For<IProductRepository>();
        _deleteProductHandler = new DeleteProductHandler(_productRepository);
    }


    [Fact(DisplayName = "Should successfully delete a product")]
    public async Task Should_successfully_delete_a_product()
    {
        //fact
        var command = DeleteProductHandlerTestData.GenerateValidCommand();

        var result = new DeleteProductResult()
        {
            Success = true
        };

        _mapper.Map<DeleteProductResult>(command).Returns(result);

        _productRepository.DeleteAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(true);

        // When
        var resultDelete = await _deleteProductHandler.Handle(command, CancellationToken.None);

        // Then
        resultDelete.Should().NotBeNull();
        resultDelete.Success.Should().Be(true);
        await _productRepository.Received(1).DeleteAsync(Arg.Any<Guid>(), CancellationToken.None);
    }


    [Fact(DisplayName = "Should insuccessfully  delete a product")]
    public async Task Should_insuccessfully_delete_a_product()
    {
        //fact
        var command = DeleteProductHandlerTestData.GenerateValidCommand();

        var result = new DeleteProductResult()
        {
            Success = false
        };

        _mapper.Map<DeleteProductResult>(command).Returns(result);

        _productRepository.DeleteAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(false);

        // Then
        await Assert.ThrowsAsync<InvalidDomainOperation>(() =>
            _deleteProductHandler.Handle(command, CancellationToken.None));
    }
}