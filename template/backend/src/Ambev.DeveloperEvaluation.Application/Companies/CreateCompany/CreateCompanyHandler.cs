using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.CreateCompany;

public class CreateCompanyHandler : IRequestHandler<CreateCompanyCommand, CreateCompanyResult>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public CreateCompanyHandler(ICompanyRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<CreateCompanyResult> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateCompanyCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var company = await _companyRepository.GetByNameAsync(request.UserId,request.Name, cancellationToken);
        
        if (company is not null)
            throw new InvalidDomainOperation($"Company with name {request.Name} already exists");

        var data = _mapper.Map<Company>(request);

        var createdCompany = await _companyRepository.CreateAsync(data, cancellationToken);
        
        var result = _mapper.Map<CreateCompanyResult>(createdCompany);
        
        return result;
    }
}