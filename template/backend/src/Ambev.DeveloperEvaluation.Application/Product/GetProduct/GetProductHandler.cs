using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Product.GetProduct;

public class GetProductHandler : IRequestHandler<GetProductQuery, GetProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductHandler(
        IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<GetProductResult> Handle(GetProductQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetProductValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = await _productRepository.GetByIdAsync(request.Id, cancellationToken);

        if (result == null)
            throw new NotFoundException($"Product with ID {request.Id} not found");

        return _mapper.Map<GetProductResult>(result);
    }
}