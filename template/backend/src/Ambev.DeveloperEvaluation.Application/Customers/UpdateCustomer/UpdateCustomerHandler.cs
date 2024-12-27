using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResult>
{
    private readonly ICustomerRepository _companyRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(ICustomerRepository companyRepository, IMapper mapper)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
    }

    public async Task<UpdateCustomerResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateCustomerCommandValidator();
        
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var company = await _companyRepository.GetByNameAsync(request.UserId,request.Name, cancellationToken);
        
        if (company is not null && company.Id != request.Id)
            throw new InvalidDomainOperation($"Customer with name {request.Name} already exists");

        var data = _mapper.Map<Customer>(request);
        
        data.UpdateDate();

        var updatedCustomer = await _companyRepository.UpdateAsync(data, cancellationToken);
        
        var result = _mapper.Map<UpdateCustomerResult>(updatedCustomer);
        
        return result;
    }
}