using System.Security.Claims;
using Ambev.DeveloperEvaluation.Application.Sale.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sale.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sale.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sale.GetListSale;
using Ambev.DeveloperEvaluation.Application.Sale.GetSale;
using Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using AutoMapper;
using Bogus;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration;

public class SalesControllerTests
{
    private readonly SalesController _controller;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly Faker _faker;

    public SalesControllerTests()
    {
        _mediator = Substitute.For<IMediator>();
        _mapper = Substitute.For<IMapper>();
        _controller = new SalesController(_mediator, _mapper);
        _faker = new Faker();
        SetCurrentUser(_faker.Random.Guid());
    }

    [Fact]
    public async Task CreateSale_ShouldReturnCreatedResult_WhenValidRequest()
    {
        // Arrange
        var request = new CreateSaleRequest
        {
            CustomerId = _faker.Random.Guid(),
            SellingCompanyId = _faker.Random.Guid(),
            SaleItems = new List<SaleItemDto>()
            {
                new(_faker.Random.Guid(), _faker.Random.Number(10), _faker.Random.Decimal())
            }
        };

        var command = new CreateSaleCommand
        {
            CustomerId = request.CustomerId,
            SellingCompanyId = request.SellingCompanyId,
            SaleItems = request.SaleItems
        };

        var saleResult = new CreateSaleResult
        {
            Id = _faker.Random.Guid(),
        };

        _mapper.Map<CreateSaleCommand>(request).Returns(command);
        _mediator.Send(Arg.Any<CreateSaleCommand>(), CancellationToken.None)
            .Returns(saleResult);

        // Act
        var result = await _controller.CreateSale(request, CancellationToken.None);

        // Assert
        var createdResult = Assert.IsType<CreatedResult>(result);
        Assert.Equal(201, createdResult.StatusCode);
    }
    
    [Fact]
    public async Task CreateSale_ShouldReturnBadRequest_WhenInvalidRequest()
    {
        // Arrange
        var request = new CreateSaleRequest();

        var command = new CreateSaleCommand();

        var expectedErrors = new List<string>
        {
            "'Selling Company Id' deve ser informado.",
            "'Customer Id' deve ser informado.",
            "'Sale Items' deve ser informado.",
            "Sale must contain at least one item"
        };
        
        _mapper.Map<CreateSaleCommand>(request).Returns(command);
        await _mediator.Send(Arg.Any<CreateSaleCommand>(), CancellationToken.None);

        // Act
        var result = await _controller.CreateSale(request, CancellationToken.None);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var responseData = ((List<ValidationFailure>)badRequest.Value!);
        
        Assert.Equal(400, badRequest.StatusCode);
        
        Assert.All(responseData, responseData =>
        {
            Assert.Contains(responseData.ErrorMessage, expectedErrors);
        });
    }

    [Fact]
    public async Task DeleteSale_ShouldReturnOk_WhenValidRequest()
    {
        // Arrange
        var saleId = _faker.Random.Guid();

        var mock = new DeleteSaleResult()
        {
            Success = true
        };

        _mediator.Send(Arg.Any<DeleteSaleCommand>(), CancellationToken.None)
            .Returns(mock);

        // Act
        var result = await _controller.DeleteSale(saleId, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
    
    [Fact]
    public async Task DeleteSale_ShouldReturnBadRequest_WhenInvalidRequest()
    {
        // Arrange
        var expectedErrors = new List<string>
        {
            "Sale ID is required",
        };

        await _mediator.Send(new DeleteSaleCommand(), CancellationToken.None);

        // Act
        var result = await _controller.DeleteSale(Guid.Empty, CancellationToken.None);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var responseData = ((List<ValidationFailure>)badRequest.Value!);

        Assert.Equal(400, badRequest.StatusCode);
        Assert.All(responseData, responseData =>
        {
            Assert.Contains(responseData.ErrorMessage, expectedErrors);
        });
    }


    [Fact]
    public async Task GetSaleById_ShouldBadRequest_WhenInvalidRequest()
    {
        // Arrange
        await _mediator.Send(new GetSaleQuery() , Arg.Any<CancellationToken>());
        var expectedErrors = new List<string>
        {
            "Sale ID is required",
        };

        // Act
        var result = await _controller.GetSaleById(Guid.Empty, CancellationToken.None);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var responseData = ((List<ValidationFailure>)badRequest.Value!);

        Assert.Equal(400, badRequest.StatusCode);
        Assert.All(responseData, responseData =>
        {
            Assert.Contains(responseData.ErrorMessage, expectedErrors);
        });
    }


    [Fact]
    public async Task UpdateSale_ShouldReturnOk_WhenValidRequest()
    {
        // Arrange

        var saleNumber = _faker.Random.String(5, 15);

        var request = new UpdateSaleRequest
        {
            SaleNumber = saleNumber,
            CustomerId = _faker.Random.Guid(),
            SellingCompanyId = _faker.Random.Guid(),
            SaleItems = new List<SaleItemUpdateDto>()
            {
                new(_faker.Random.Guid(), _faker.Random.Guid(), 2, 50)
            }
        };

        var updateResult = new UpdateSaleResult
        {
            Id = request.Id,
            Amount = 100,
            SaleItems = new List<SaleItemResultDto>()
            {
                new(_faker.Random.Guid(),_faker.Random.Guid(), 2, 0, 50)
            }
        };

        _mapper.Map<UpdateSaleCommand>(request).Returns(new UpdateSaleCommand());
        _mediator.Send(Arg.Any<UpdateSaleCommand>(), Arg.Any<CancellationToken>())
            .Returns(updateResult);

        // Act
        var result = await _controller.UpdateSale(request, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var responseData = ((ApiResponseWithData<UpdateSaleResult>)okResult.Value!)?.Data;
        Assert.Equal(request.Id, responseData!.Id);
        Assert.Equal(100, updateResult.Amount);
    }

    [Fact]
    public async Task CancelSale_ShouldReturnOk_WhenValidRequest()
    {
        // Arrange
        var saleId = _faker.Random.Guid();

        var cancelResult = new CancelSaleResult
        {
            Id = saleId,
            Cancel = true
        };

        _mediator.Send(Arg.Any<CancelSaleCommand>(), Arg.Any<CancellationToken>())
            .Returns(cancelResult);

        // Act
        var result = await _controller.CancelSale(saleId, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(200, okResult.StatusCode);
    }
    
    [Fact]
    public async Task CancelSale_ShouldReturnBadRequest_WhenInvalidRequest()
    {
        // Arrange
        await _mediator.Send(Arg.Any<CancelSaleCommand>(), Arg.Any<CancellationToken>());
        var expectedErrors = new List<string>
        {
            "Sale ID is required",
        };

        // Act
        var result = await _controller.CancelSale(Guid.Empty, CancellationToken.None);

        // Assert
        var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        var responseData = ((List<ValidationFailure>)badRequest.Value!);

        Assert.Equal(400, badRequest.StatusCode);
        Assert.All(responseData, responseData =>
        {
            Assert.Contains(responseData.ErrorMessage, expectedErrors);
        });
    }


    [Fact]
    public async Task SearchAsync_ShouldReturnOk_WhenRequestIsValid()
    {
        // Arrange
        var page = 1;
        var size = 10;

        var resultCommand = new PaginatedList<GetListSaleResult>()
        {
            new()
            {
                SellingCompanyId = _faker.Random.Guid(),
                SaleItems = new List<SaleItemResultDto>()
                {
                    new(_faker.Random.Guid(),_faker.Random.Guid(), _faker.Random.Number(10), _faker.Random.Decimal(),
                        _faker.Finance.Random.Decimal())
                }
            }
        };

        _mediator.Send(Arg.Any<GetListSaleQuery>(), Arg.Any<CancellationToken>()).Returns(resultCommand);

        // Act
        var result = await _controller.SearchAsync(page, size, string.Empty, CancellationToken.None);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var paginatedResponse = ((PaginatedResponse<List<GetListSaleResult>>)okResult.Value!);
        Assert.Single(paginatedResponse.Data ?? new List<GetListSaleResult>());
    }

    private void SetCurrentUser(Guid userId)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Name, _faker.Name.FullName())
        };

        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = principal }
        };
    }
}