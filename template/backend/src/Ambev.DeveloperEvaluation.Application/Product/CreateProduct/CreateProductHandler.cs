using Ambev.DeveloperEvaluation.Application.Product.Events;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.CreateProduct;

public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CreateProductHandler(IProductRepository productRepository, IMapper mapper, IMediator mediator)
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _mediator = mediator;
    }

    public async Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateProductCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = await _productRepository.GetByNameAsync(request.UserId, request.Name, cancellationToken);

        if (product is not null)
            throw new InvalidDomainOperation($"Product with name {request.Name} already exists");

        var data = _mapper.Map<Domain.Entities.Product>(request);

        var createdProduct = await _productRepository.CreateAsync(data, cancellationToken);

        await _mediator.Publish(new ProductCreatedEvent(createdProduct), cancellationToken);
        
        return _mapper.Map<CreateProductResult>(createdProduct);
    }
}