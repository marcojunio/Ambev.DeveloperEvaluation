using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.DeleteCompany;

public class DeleteCompanyHandler : IRequestHandler<DeleteCompanyCommand, DeleteCompanyResult>
{
    private readonly ICompanyRepository _companyRepository;

    public DeleteCompanyHandler(
        ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<DeleteCompanyResult> Handle(DeleteCompanyCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteCompanyValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _companyRepository.DeleteAsync(request.Id, cancellationToken);
        
        if (!success)
            throw new InvalidDomainOperation($"Company with ID {request.Id} not found");

        return new DeleteCompanyResult { Success = true };
    }
}
