using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper,IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request);

        ConsolidateSaleItems(request);

        var data = _mapper.Map<Domain.Entities.Sale>(request);

        data.VerifyItemsAndApplyCalculate();
        
        var createdSale = await _saleRepository.CreateAsync(data, cancellationToken);

        await _mediator.Publish(new SaleCreatedEvent(data,DateTime.UtcNow), cancellationToken);
        
        return _mapper.Map<CreateSaleResult>(createdSale);
    }

    private async Task ValidateRequest(CreateSaleCommand request)
    {
        var validator = new CreateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
    }

    private static void ConsolidateSaleItems(CreateSaleCommand command)
    {
        var aggregatedItems = command.SaleItems
            .GroupBy(item => item.ProductId)
            .Select(group => new SaleItemDto(group.Key, group.Sum(item => item.Quantity),group.Sum(item => item.UnitPrice)))
            .ToList();

        command.SaleItems = aggregatedItems;
    }
}