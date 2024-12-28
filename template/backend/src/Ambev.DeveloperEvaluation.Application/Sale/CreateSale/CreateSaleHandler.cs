using Ambev.DeveloperEvaluation.Application.Sale.Events;
using Ambev.DeveloperEvaluation.Domain.Dtos;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.CreateSale;

public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateSaleHandler(ISaleRepository saleRepository, IMapper mapper,
        IProductRepository productRepository, ICustomerRepository customerRepository,
        ICompanyRepository companyRepository, IMediator mediator)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _productRepository = productRepository;
        _customerRepository = customerRepository;
        _companyRepository = companyRepository;
        _mediator = mediator;
    }

    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        await ValidateRequest(request);

        ConsolidateSaleItems(request);

        var data = _mapper.Map<Domain.Entities.Sale>(request);

        var products = await GetProductsForSale(data);

        ValidateAndMapSaleItems(data.Items, products, request.UserId);

        data.CalculateAmount();

        var createdSale = await _saleRepository.CreateAsync(data, cancellationToken);

        await _mediator.Publish(new SaleCreatedEvent(data), cancellationToken);
        
        return _mapper.Map<CreateSaleResult>(createdSale);
    }

    private async Task ValidateRequest(CreateSaleCommand request)
    {
        var validator = new CreateSaleCommandValidator();
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
            throw new InvalidDomainOperation("Not found products for sale");

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
                          throw new InvalidDomainOperation($"Product with ID {item.ProductId} was not found");

            item.UserId = userId;
            item.Product = product;

            item.CanSale();

            item.ApplyDiscount();
        }
    }

    private static void ConsolidateSaleItems(CreateSaleCommand command)
    {
        var aggregatedItems = command.SaleItems
            .GroupBy(item => item.ProductId)
            .Select(group => new SaleItemDto(group.Key, group.Sum(item => item.Quantity)))
            .ToList();

        command.SaleItems = aggregatedItems;
    }
}