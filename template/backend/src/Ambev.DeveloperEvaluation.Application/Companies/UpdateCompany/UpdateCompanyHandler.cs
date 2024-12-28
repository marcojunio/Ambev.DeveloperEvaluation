using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.UpdateCompany;

public class UpdateCompanyHandler : IRequestHandler<UpdateCompanyCommand, UpdateCompanyResult>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public UpdateCompanyHandler(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<UpdateCompanyResult> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateCompanyCommandValidator();
        
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var company = await _companyRepository.GetByNameAsync(request.UserId,request.Name, cancellationToken);
        
        if (company is not null && company.Id != request.Id)
            throw new InvalidDomainOperation($"Company with name {request.Name} already exists");

        var existingCompany = await _companyRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (existingCompany == null)
            throw new NotFoundException($"Customer with ID {request.Id} not found.");
        
        var data = _mapper.Map(request,existingCompany);
        
        data.UpdateDate();

        var updatedCompany = await _companyRepository.UpdateAsync(data, cancellationToken);
        
        var result = _mapper.Map<UpdateCompanyResult>(updatedCompany);
        
        return result;
    }
}