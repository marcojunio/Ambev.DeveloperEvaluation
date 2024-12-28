using Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Company.TestData;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company;

public class DeleteCompanyHandleTests
{
    private readonly  IMapper _mapper;
    private readonly  ICompanyRepository _companyRepository;
    private readonly  DeleteCompanyHandler _deleteCompanyHandler;

    public DeleteCompanyHandleTests()
    {
        _mapper = Substitute.For<IMapper>();
        _companyRepository = Substitute.For<ICompanyRepository>();
        _deleteCompanyHandler = new DeleteCompanyHandler(_companyRepository);
    }


    [Fact(DisplayName = "Should successfully delete a company")]
    public async Task Should_successfully_delete_a_company()
    {
        //fact
        var command = DeleteCompanyHandlerTestData.GenerateValidCommand();

        var result = new DeleteCompanyResult()
        {
            Success = true
        };

        _mapper.Map<DeleteCompanyResult>(command).Returns(result);
        
        _companyRepository.DeleteAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(true);
        
        // When
        var resultDelete = await _deleteCompanyHandler.Handle(command, CancellationToken.None);
        
        // Then
        resultDelete.Should().NotBeNull();
        resultDelete.Success.Should().Be(true);
        await _companyRepository.Received(1).DeleteAsync(Arg.Any<Guid>(), CancellationToken.None);
    }
    
    
    [Fact(DisplayName = "Should insuccessfully  delete a company")]
    public async Task Should_insuccessfully_delete_a_company()
    {
        //fact
        var command = DeleteCompanyHandlerTestData.GenerateValidCommand();

        var result = new DeleteCompanyResult()
        {
            Success = false
        };

        _mapper.Map<DeleteCompanyResult>(command).Returns(result);
        
        _companyRepository.DeleteAsync(Arg.Any<Guid>(), CancellationToken.None).Returns(false);
        
        // Then
        await Assert.ThrowsAsync<InvalidDomainOperation>( () => _deleteCompanyHandler.Handle(command, CancellationToken.None));
    }
}