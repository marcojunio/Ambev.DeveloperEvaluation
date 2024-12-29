using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using Bogus;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sale;

public class CreateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _mediator = Substitute.For<IMediator>();
        _handler = new CreateSaleHandler(_saleRepository, _mapper, _mediator);
    }

    [Fact]
    public async Task Handle_Should_Create_Sale_When_Valid_Request()
    {
        // Arrange
        var faker = new Faker();

        var command = new CreateSaleCommand
        {
            CustomerId = faker.Random.Guid(),
            SellingCompanyId = faker.Random.Guid(),
            UserId = faker.Random.Guid(),
            SaleItems = new List<SaleItemDto>
            {
                new(faker.Random.Guid(), faker.Random.Int(1, 10), faker.Finance.Amount(10, 100)),
                new(faker.Random.Guid(), faker.Random.Int(1, 10), faker.Finance.Amount(10, 100))
            }
        };

        var saleEntity = new DeveloperEvaluation.Domain.Entities.Sale
        {
            CustomerId = command.CustomerId,
            SellingCompanyId = command.SellingCompanyId,
            UserId = command.UserId
        };

        var saleResult = new CreateSaleResult { Id = faker.Random.Guid() };

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Sale>(command).Returns(saleEntity);
        _saleRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), CancellationToken.None).Returns(saleEntity);
        _mapper.Map<CreateSaleResult>(saleEntity).Returns(saleResult);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Equal(saleResult.Id, result.Id);
        await _saleRepository.Received(1).CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), CancellationToken.None);
        await _mediator.Received(1).Publish(Arg.Any<SaleCreatedEvent>(), CancellationToken.None);
    }

    [Fact]
    public async Task Handle_Should_Throw_ValidationException_When_Request_Is_Invalid()
    {
        // Arrange
        var command = new CreateSaleCommand
        {
            CustomerId = Guid.Empty,
            SellingCompanyId = Guid.Empty,
            UserId = Guid.Empty,
            SaleItems = new List<SaleItemDto>()
        };

        // Act & Assert
        await Assert.ThrowsAsync<FluentValidation.ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        await _saleRepository.DidNotReceive().CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), CancellationToken.None);
        await _mediator.DidNotReceive().Publish(Arg.Any<SaleCreatedEvent>(), CancellationToken.None);
    }

    [Fact]
    public async Task Handle_Should_Consolidate_SaleItems_Before_Processing()
    {
        // Arrange
        var faker = new Faker();
        var productId = faker.Random.Guid();

        var command = new CreateSaleCommand
        {
            CustomerId = faker.Random.Guid(),
            SellingCompanyId = faker.Random.Guid(),
            UserId = faker.Random.Guid(),
            SaleItems = new List<SaleItemDto>
            {
                new(productId, 2, 10.00m),
                new(productId, 3, 20.00m)
            }
        };

        var saleEntity = new DeveloperEvaluation.Domain.Entities.Sale();

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Sale>(Arg.Any<CreateSaleCommand>()).Returns(saleEntity);
        _saleRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Sale>(), CancellationToken.None)
            .Returns(saleEntity);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.Single(command.SaleItems);
        Assert.Equal(productId, command.SaleItems.First().ProductId);
        Assert.Equal(5, command.SaleItems.First().Quantity);
        Assert.Equal(30.00m, command.SaleItems.First().UnitPrice);
    }
}
