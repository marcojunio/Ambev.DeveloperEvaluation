using Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Customer.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customer;

public class CreateCustomerHandlerTests
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly CreateCustomerHandler _handler;

    public CreateCustomerHandlerTests()
    {
        _customerRepository = Substitute.For<ICustomerRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateCustomerHandler(_customerRepository, _mapper,Substitute.For<IMediator>());
    }

    [Fact(DisplayName = "Given valid customer data When creating user Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateCustomerHandlerTestData.GenerateValidCommand();

        var customer = new DeveloperEvaluation.Domain.Entities.Customer()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            UserId = command.UserId,
            Age = 25
        };

        var result = new CreateCustomerResult()
        {
            Id = customer.Id,
        };

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Customer>(command).Returns(customer);
        _mapper.Map<CreateCustomerResult>(customer).Returns(result);

        _customerRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Customer>(), CancellationToken.None)
            .Returns(customer);

        // When
        var createCustomerResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createCustomerResult.Should().NotBeNull();
        createCustomerResult.Id.Should().Be(customer.Id);
        await _customerRepository.Received(1).CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Customer>(),
            CancellationToken.None);
    }

    [Fact(DisplayName = "Given invalid customer data When creating user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateCustomerCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Given valid command When handling Then maps command to user entity")]
    public async Task Handle_ValidRequest_MapsCommandToCustomer()
    {
        // Given
        var command = CreateCustomerHandlerTestData.GenerateValidCommand();

        var customer = new DeveloperEvaluation.Domain.Entities.Customer()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            UserId = command.UserId,
            Age = 25
        };

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Customer>(command).Returns(customer);

        _customerRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Customer>(), CancellationToken.None)
            .Returns(customer);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<DeveloperEvaluation.Domain.Entities.Customer>(Arg.Is<CreateCustomerCommand>(c =>
            c.Name == command.Name &&
            c.Age == command.Age &&
            c.UserId == command.UserId));
    }
}