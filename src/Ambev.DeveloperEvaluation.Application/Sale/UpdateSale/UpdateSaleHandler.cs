using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.UpdateSale;

public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request);

        ConsolidateSaleItems(request);

        var existingSale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

        if (existingSale == null)
            throw new NotFoundException($"Sale with ID {request.Id} not found.");

        var data = _mapper.Map(request, existingSale);

        data.VerifyItemsAndApplyCalculate(true);

        data.UpdateDate();

        var updatedSale = await _saleRepository.UpdateAsync(data, cancellationToken);

        await _mediator.Publish(new SaleModifiedEvent(existingSale,DateTime.UtcNow), cancellationToken);
        
        
        if(updatedSale is not null)
            await _mediator.Publish(new ItemCancelledEvent(existingSale, updatedSale,DateTime.UtcNow), cancellationToken);

        return _mapper.Map<UpdateSaleResult>(updatedSale);
    }

    private async Task ValidateRequest(UpdateSaleCommand request)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }

    private static void ConsolidateSaleItems(UpdateSaleCommand command)
    {
        var uniqueItems = command.SaleItems
            .GroupBy(item => item.ProductId)
            .Select(group =>
            {
                var firstItem = group.First();
                return firstItem with { Quantity = group.Sum(item => item.Quantity) };
            })
            .ToList();

        command.SaleItems = uniqueItems;
    }
}