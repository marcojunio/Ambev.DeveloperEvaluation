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
    private readonly IProductRepository _productRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UpdateSaleHandler(ISaleRepository saleRepository, IMapper mapper, IProductRepository productRepository,
        ICompanyRepository companyRepository, ICustomerRepository customerRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _productRepository = productRepository;
        _companyRepository = companyRepository;
        _customerRepository = customerRepository;
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

        var products = await GetProductsForSale(data);

        ValidateAndMapSaleItems(data.Items, products, request.UserId);

        data.CalculateAmount();

        data.UpdateDate();

        var updatedSale = await _saleRepository.UpdateAsync(data, cancellationToken);

        await _mediator.Publish(new SaleModifiedEvent(existingSale), cancellationToken);
        
        
        if(updatedSale is not null)
            await _mediator.Publish(new ItemCancelledEvent(existingSale, updatedSale), cancellationToken);

        return _mapper.Map<UpdateSaleResult>(updatedSale);
    }

    private async Task ValidateRequest(UpdateSaleCommand request)
    {
        var validator = new UpdateSaleCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        if (!await _customerRepository.ExistCustomerAsync(request.CustomerId))
            throw new NotFoundException($"Not found custumer with ID {request.CustomerId}");

        if (!await _companyRepository.ExistCompanyAsync(request.SellingCompanyId))
            throw new NotFoundException($"Not found company with ID {request.SellingCompanyId}");
    }

    private async Task<List<Domain.Entities.Product>> GetProductsForSale(Domain.Entities.Sale data)
    {
        var productIds = data.Items.Select(f => f.ProductId).ToList();

        var products = await _productRepository.GetProductsByIds(data.UserId, productIds);

        if (products.Count == 0)
            throw new NotFoundException("Not found products for sale");

        return products;
    }

    private static void ValidateAndMapSaleItems(
        IEnumerable<SaleItem> items,
        IReadOnlyCollection<Domain.Entities.Product> products,
        Guid userId)
    {
        foreach (var item in items)
        {
            var product = products.SingleOrDefault(f => f.Id == item.ProductId) ??
                          throw new NotFoundException($"Product with ID {item.ProductId} was not found");

            item.UserId = userId;

            item.Product = product;

            item.UpdateDate();

            item.CanSale();

            item.ApplyDiscount();
        }
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