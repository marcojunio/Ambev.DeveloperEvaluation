using Ambev.DeveloperEvaluation.Application.Companies.GetCompany;
using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Company.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company;

public class GetCompanyHandlerTests
{
    private readonly  IMapper _mapper;
    private readonly  ICompanyRepository _companyRepository;
    private readonly  GetCompanyHandler _getCompanyHandler;

    public GetCompanyHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _companyRepository = Substitute.For<ICompanyRepository>();
        _getCompanyHandler = new GetCompanyHandler(_companyRepository, _mapper,Substitute.For<ICacheService>());
    }
    
    
    [Fact(DisplayName = "Should successfully get a company")]
    public async Task Should_successfully_get_a_company()
    {
        //fact
        var command = GetCompanyHandlerTestData.GenerateValidCommand();

        var company = new DeveloperEvaluation.Domain.Entities.Company
        {
            Id = Guid.NewGuid(),
            Name = "Test"
        };
        
        var result = new GetCompanyResult()
        {
            Id = Guid.NewGuid(),
            Name = "Test"
        };

        _mapper.Map<GetCompanyResult>(Arg.Any<DeveloperEvaluation.Domain.Entities.Company>()).Returns(result);
        
        _companyRepository.GetByIdAsync(Arg.Any<Guid>(), Arg.Is(CancellationToken.None)).Returns(company);

        // When
        var resultHandler = await _getCompanyHandler.Handle(command, CancellationToken.None);
        
        //Then
        Assert.Equal(result.Id,resultHandler.Id);
        Assert.Equal(result.Name,resultHandler.Name);
    }
    
    [Fact(DisplayName = "Should insuccessfully get a company")]
    public async Task Should_insuccessfully_get_a_company()
    {
        //fact
        var command = new GetCompanyQuery();

        // When
        var act = () =>  _getCompanyHandler.Handle(command, CancellationToken.None);
        
        //Then
        await act.Should().ThrowAsync<ValidationException>();
    }
    
    [Fact(DisplayName = "Shouldn't find a company")]
    public async Task Shouldnt_find_a_company()
    {
        //fact
        var command = new GetCompanyQuery(Guid.NewGuid());
        
        // When
        var act = async () => await _getCompanyHandler.Handle(command, CancellationToken.None);
        
        //Then
        await act.Should().ThrowAsync<NotFoundException>()
            .WithMessage($"Company with ID {command.Id} not found");
    }
}