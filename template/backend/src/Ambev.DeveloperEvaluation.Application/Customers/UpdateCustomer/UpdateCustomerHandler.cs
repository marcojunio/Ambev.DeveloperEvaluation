using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.UpdateCustomer;

public class UpdateCustomerHandler : IRequestHandler<UpdateCustomerCommand, UpdateCustomerResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public UpdateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<UpdateCustomerResult> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateCustomerCommandValidator();
        
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var customer = await _customerRepository.GetByNameAsync(request.UserId,request.Name, cancellationToken);
        
        if (customer is not null && customer.Id != request.Id)
            throw new InvalidDomainOperation($"Customer with name {request.Name} already exists");

        var existingCustomer = await _customerRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (existingCustomer == null)
            throw new NotFoundException($"Customer with ID {request.Id} not found.");
        
        var data = _mapper.Map(request,existingCustomer);
        
        data.UpdateDate();

        var updatedCustomer = await _customerRepository.UpdateAsync(data, cancellationToken);
        
        var result = _mapper.Map<UpdateCustomerResult>(updatedCustomer);
        
        return result;
    }
}