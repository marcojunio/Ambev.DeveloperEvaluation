using Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Customer.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Customer;

public class GetCustomerHandlerTests
{
    private readonly  IMapper _mapper;
    private readonly  ICustomerRepository _customerRepository;
    private readonly  GetCustomerHandler _getCustomerHandler;

    public GetCustomerHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _customerRepository = Substitute.For<ICustomerRepository>();
        _getCustomerHandler = new GetCustomerHandler(_customerRepository, _mapper);
    }
    
    
    [Fact(DisplayName = "Should successfully get a customer")]
    public async Task Should_successfully_get_a_customer()
    {
        //fact
        var command = GetCustomerHandlerTestData.GenerateValidCommand();

        var customer = new DeveloperEvaluation.Domain.Entities.Customer
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Age = 25
        };
        
        var result = new GetCustomerResult()
        {
            Id = Guid.NewGuid(),
            Name = "Test",
            Age = 25
        };

        _mapper.Map<GetCustomerResult>(Arg.Any<DeveloperEvaluation.Domain.Entities.Customer>()).Returns(result);
        
        _customerRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Is(CancellationToken.None)).Returns(customer);

        // When
        var resultHandler = await _getCustomerHandler.Handle(command, CancellationToken.None);
        
        //Then
        Assert.Equal(result.Id,resultHandler.Id);
        Assert.Equal(result.Name,resultHandler.Name);
    }
    
    [Fact(DisplayName = "Should insuccessfully get a customer")]
    public async Task Should_insuccessfully_get_a_customer()
    {
        //fact
        var command = new GetCustomerQuery();

        // When
        var act = () =>  _getCustomerHandler.Handle(command, CancellationToken.None);
        
        //Then
        await act.Should().ThrowAsync<ValidationException>();
    }
    
    [Fact(DisplayName = "Shouldn't find a customer")]
    public async Task Shouldnt_find_a_customer()
    {
        //fact
        var command = new GetCustomerQuery(Guid.NewGuid());
        
        // When
        var act = async () => await _getCustomerHandler.Handle(command, CancellationToken.None);
        
        //Then
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Customer with ID {command.Id} not found");
    }
}