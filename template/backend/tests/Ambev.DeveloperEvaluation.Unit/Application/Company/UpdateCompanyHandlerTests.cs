using Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.Company.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Company;

public class UpdateCompanyHandlerTests
{
    private readonly IMapper _mapper;
    private readonly ICompanyRepository _companyRepository;
    private readonly UpdateCompanyHandler _handler;

    public UpdateCompanyHandlerTests()
    {
        _mapper = Substitute.For<IMapper>();
        _companyRepository = Substitute.For<ICompanyRepository>();
        _handler = new UpdateCompanyHandler(_companyRepository, _mapper);
    }

    [Fact(DisplayName = "Should update with success a company")]
    public async Task Should_update_with_success_a_company()
    {
        //Fact
        var command = UpdateCompanyHandlerTestData.GenerateValidCommand();

        var date = DateTime.UtcNow;

        var company = new DeveloperEvaluation.Domain.Entities.Company()
        {
            Id = Guid.NewGuid(),
            Name = "Test"
        };

        var result = new UpdateCompanyResult()
        {
            Id = Guid.NewGuid(),
            CreatedAt = date,
            Name = "Test update"
        };

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Company>(command).Returns(company);
        _mapper.Map<UpdateCompanyResult>(Arg.Any<DeveloperEvaluation.Domain.Entities.Company>()).Returns(result);

        _companyRepository
            .UpdateAsync(Arg.Any<DeveloperEvaluation.Domain.Entities.Company>(), CancellationToken.None).Returns(
                new DeveloperEvaluation.Domain.Entities.Company()
                {
                    Id = result.Id,
                    Name = result.Name,
                    UpdatedAt = result.UpdatedAt
                });

        //When
        var resultHandler = await _handler.Handle(command, CancellationToken.None);

        //Then
        Assert.NotEqual(company.Name, resultHandler.Name);
        company.UpdatedAt.Should().NotBeNull();
    }

    [Fact(DisplayName = "Should update with exception a company")]
    public async Task Should_update_with_failure_a_company()
    {
        //Fact
        var command = new UpdateCompanyCommand();

        //When
        var act = () => _handler.Handle(command, CancellationToken.None);

        //Then
        await act.Should().ThrowAsync<ValidationException>();
    }

    [Fact(DisplayName = "Should update with exception a company because name exist")]
    public async Task Should_update_with_failure_a_company_because_name_exist()
    {
        // Fact
        var command = UpdateCompanyHandlerTestData.GenerateValidCommand();

        var existingCompany = new DeveloperEvaluation.Domain.Entities.Company
        {
            Id = Guid.NewGuid(),
            Name = command.Name
        };

        _companyRepository
            .GetByNameAsync(command.UserId, command.Name, CancellationToken.None)
            .Returns(existingCompany);

        _mapper.Map<DeveloperEvaluation.Domain.Entities.Company>(command).Returns(existingCompany);

        var act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<InvalidDomainOperation>()
            .WithMessage($"Company with name {command.Name} already exists");
    }
}