using Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Customer.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customer;

public class UpdateCustomerHandlerTests
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;
    private readonly UpdateCustomerHandler _handler;

    public UpdateCustomerHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _customerRepository = Substitute.For<ICustomerRepository>();
        _handler = new UpdateCustomerHandler(_customerRepository, _mapper);
    }

    [Fact(DisplayName = "Should update with success a customer")]
    public async Task Should_update_with_success_a_customer()
    {
        //Fact
        var command = UpdateCustomerHandlerTestData.GenerateValidCommand();

        var date = DateTime.UtcNow;

        var customer = new DeveloperEvaluation.Domain.Entities.Customer()
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Age = 25
        };

        var result = new UpdateCustomerResult()
        {
            Id = Guid.NewGuid(),
            CreatedAt = date,
            Name = "Test update",
            Age = 26
        };

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Customer>(command).Returns(customer);
        _mapper.Map<UpdateCustomerResult>(Arg.Any<DeveloperEvaluation.Domain.Entities.Customer>()).Returns(result);

        _customerRepository
            .UpdateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Customer>(), CancellationToken.None).Returns(
                new DeveloperEvaluation.Domain.Entities.Customer()
                {
                    Id = result.Id,
                    Name = result.Name,
                    UpdatedAt = result.UpdatedAt
                });

        //When
        var resultHandler = await _handler.Handle(command, CancellationToken.None);

        //Then
        Assert.NotEqual(customer.Name, resultHandler.Name);
        Assert.NotEqual(customer.Age, resultHandler.Age);
        customer.UpdatedAt.Should().NotBeNull();
    }

    [Fact(DisplayName = "Should update with exception a customer")]
    public async Task Should_update_with_failure_a_customer()
    {
        //Fact
        var command = new UpdateCustomerCommand();

        //When
        var act = () => _handler.Handle(command, CancellationToken.None);

        //Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Should update with exception a customer because name exist")]
    public async Task Should_update_with_failure_a_customer_because_name_exist()
    {
        // Fact
        var command = UpdateCustomerHandlerTestData.GenerateValidCommand();

        var existingCustomer = new DeveloperEvaluation.Domain.Entities.Customer
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            Age = 25
        };

        _customerRepository
            .GetByNameAsync(command.UserId, command.Name, CancellationToken.None)
            .Returns(existingCustomer);

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Customer>(command).Returns(existingCustomer);

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidDomainOperation>()
            .WithMessage($"Customer with name {command.Name} already exists");
    }
}