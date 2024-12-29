using Ambev.DeveloperEvaluation.Application.Customers.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.DeleteCustomer;

public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, DeleteCustomerResult>
{
    private readonly ICustomerRepository _companyRepository;
    private readonly IMediator _mediator;

    public DeleteCustomerHandler(
        ICustomerRepository companyRepository, IMediator mediator)
    {
        _companyRepository = companyRepository;
        _mediator = mediator;
    }

    public async Task<DeleteCustomerResult> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteCustomerValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var success = await _companyRepository.DeleteAsync(request.Id, cancellationToken);

        if (!success)
            throw new InvalidDomainOperation($"Customer with ID {request.Id} not found");

        await _mediator.Publish(new CustomerDeletedEvent(request.Id), cancellationToken);
        
        return new DeleteCustomerResult { Success = true };
    }
}