using Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Customer.TestData;
using AutoMapper;
using FluentAssertions;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customer;

public class DeleteCustomerHandleTests
{
    private readonly IMapper _mapper;
    private readonly ICustomerRepository _customerRepository;
    private readonly DeleteCustomerHandler _deleteCustomerHandler;

    public DeleteCustomerHandleTests()
    {
        _mapper = Substitute.For<IMapper>();
        _customerRepository = Substitute.For<ICustomerRepository>();
        
        _deleteCustomerHandler = new DeleteCustomerHandler(_customerRepository, Substitute.For<IMediator>());
    }


    [Fact(DisplayName = "Should successfully delete a customer")]
    public async Task Should_successfully_delete_a_customer()
    {
        //fact
        var command = DeleteCustomerHandlerTestData.GenerateValidCommand();

        var result = new DeleteCustomerResult()
        {
            Success = true
        };

        _mapper.Map<DeleteCustomerResult>(command).Returns(result);

        _customerRepository.DeleteAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(true);

        // When
        var resultDelete = await _deleteCustomerHandler.Handle(command, CancellationToken.None);

        // Then
        resultDelete.Should().NotBeNull();
        resultDelete.Success.Should().Be(true);
        await _customerRepository.Received(1).DeleteAsync(Arg.Any<Guid>(), CancellationToken.None);
    }


    [Fact(DisplayName = "Should insuccessfully  delete a customer")]
    public async Task Should_insuccessfully_delete_a_customer()
    {
        //fact
        var command = DeleteCustomerHandlerTestData.GenerateValidCommand();

        var result = new DeleteCustomerResult()
        {
            Success = false
        };

        _mapper.Map<DeleteCustomerResult>(command).Returns(result);

        _customerRepository.DeleteAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(false);

        // Then
        await Assert.ThrowsAsync<InvalidDomainOperation>(() =>
            _deleteCustomerHandler.Handle(command, CancellationToken.None));
    }
}