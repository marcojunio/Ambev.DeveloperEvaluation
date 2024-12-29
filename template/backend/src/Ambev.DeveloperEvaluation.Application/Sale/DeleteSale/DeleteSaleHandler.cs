using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.DeleteSale;

public class DeleteSaleHandler : IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IEventPublisher _eventPublisher;

    public DeleteSaleHandler(
        ISaleRepository saleRepository, IMediator mediator, IEventPublisher eventPublisher)
    {
        _saleRepository = saleRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        //var success = await _saleRepository.DeleteAsync(request.Id, cancellationToken);

        if (!true)
            throw new InvalidDomainOperation($"Sale with ID {request.Id} not found");

        await _eventPublisher.PublishEventAsync(new SaleDeletedEvent(request.Id, request.UserId, DateTime.UtcNow), cancellationToken);

        return new DeleteSaleResult { Success = true };
    }
}