using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.UpdateProduct;

public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductCommandValidator();
        
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = await _productRepository.GetByNameAsync(request.UserId,request.Name, cancellationToken);
        
        if (product is not null && product.Id != request.Id)
            throw new InvalidDomainOperation($"Product with name {request.Name} already exists");

        var existingProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (existingProduct == null)
            throw new NotFoundException($"Product with ID {request.Id} not found.");
        
        var data = _mapper.Map(request,existingProduct);
        
        data.UpdateDate();

        var updatedProduct = await _productRepository.UpdateAsync(data, cancellationToken);
        
        var result = _mapper.Map<UpdateProductResult>(updatedProduct);
        
        return result;
    }
}