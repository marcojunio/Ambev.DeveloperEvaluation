using Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Company.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using MediatR;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company;

public class CreateCompanyHandlerTests
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;
    private readonly CreateCompanyHandler _handler;

    public CreateCompanyHandlerTests()
    {
        _companyRepository = Substitute.For<ICompanyRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateCompanyHandler(_companyRepository, _mapper,Substitute.For<IMediator>());
    }
    
    [Fact(DisplayName = "Given valid company data When creating user Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Given
        var command = CreateCompanyHandlerTestData.GenerateValidCommand();
        
        var company = new DeveloperEvaluation.Domain.Entities.Company()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            UserId = command.UserId
        };

        var result = new CreateCompanyResult()
        {
            Id = company.Id,
        };

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Company>(command).Returns(company);
        _mapper.Map<CreateCompanyResult>(company).Returns(result);
        
        _companyRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Company>(), CancellationToken.None).Returns(company);

        // When
        var createCompanyResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        createCompanyResult.Should().NotBeNull();
        createCompanyResult.Id.Should().Be(company.Id);
        await _companyRepository.Received(1).CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Company>(), CancellationToken.None);
    }
    
    [Fact(DisplayName = "Given invalid company data When creating user Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Given
        var command = new CreateCompanyCommand();

        // When
        var act = () => _handler.Handle(command, CancellationToken.None);

        // Then
        await act.Should().ThrowAsync<ValidationException>();
    }
    
    [Fact(DisplayName = "Given valid command When handling Then maps command to user entity")]
    public async Task Handle_ValidRequest_MapsCommandToCompany()
    {
        // Given
        var command = CreateCompanyHandlerTestData.GenerateValidCommand();
        
        var company = new DeveloperEvaluation.Domain.Entities.Company()
        {
            Id = Guid.NewGuid(),
            Name = command.Name,
            UserId = command.UserId
        };

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Company>(command).Returns(company);
        
        _companyRepository.CreateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Company>(), CancellationToken.None)
            .Returns(company);

        // When
        await _handler.Handle(command, CancellationToken.None);

        // Then
        _mapper.Received(1).Map<DeveloperEvaluation.Domain.Entities.Company>(Arg.Is<CreateCompanyCommand>(c =>
            c.Name == command.Name &&
            c.UserId == command.UserId));
    }
}