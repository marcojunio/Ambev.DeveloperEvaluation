using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CancelSale;

public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IEventPublisher _eventPublisher;

    public CancelSaleHandler(
        ISaleRepository saleRepository, IMediator mediator, IEventPublisher eventPublisher)
    {
        _saleRepository = saleRepository;
        _eventPublisher = eventPublisher;
    }

    public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new CancelSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken)
                   ?? throw new NotFoundException($"Sale with ID {request.Id} not found.");

        sale.CancelSale();

        await _saleRepository.UpdateAsync(sale, cancellationToken);

        await _eventPublisher.PublishEventAsync(new SaleCancelledEvent(sale, DateTime.UtcNow), cancellationToken);

        return new CancelSaleResult
        {
            Cancel = true,
            Id = request.Id
        };
    }
}