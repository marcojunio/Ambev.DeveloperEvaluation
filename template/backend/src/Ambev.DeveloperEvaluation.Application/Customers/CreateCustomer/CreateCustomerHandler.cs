using Ambev.DeveloperEvaluation.Application.Customers.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.CreateCustomer;

public class CreateCustomerHandler : IRequestHandler<CreateCustomerCommand, CreateCustomerResult>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateCustomerHandler(ICustomerRepository customerRepository, IMapper mapper, IMediator mediator)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<CreateCustomerResult> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateCustomerCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var customer = await _customerRepository.GetByNameAsync(request.UserId, request.Name, cancellationToken);

        if (customer is not null)
            throw new InvalidDomainOperation($"Customer with name {request.Name} already exists");

        var data = _mapper.Map<Customer>(request);

        var createdCustomer = await _customerRepository.CreateAsync(data, cancellationToken);

        await _mediator.Publish(new CustomerCreatedEvent(createdCustomer), cancellationToken);

        return _mapper.Map<CreateCustomerResult>(createdCustomer);
    }
}