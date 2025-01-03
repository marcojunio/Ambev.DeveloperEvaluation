using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetSale;

public class GetSaleHandler : IRequestHandler<GetSaleQuery, GetSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetSaleHandler(
        ISaleRepository saleRepository, IMapper mapper, ICacheService cacheService)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<GetSaleResult> Handle(GetSaleQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetSaleValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = await _cacheService.GetOrCreateAsync(CacheKeys.GetSaleKey(request.Id),
            async () => await _saleRepository.GetByIdAsync(request.Id, cancellationToken));

        if (result == null)
            throw new NotFoundException($"Sale with ID {request.Id} not found");

        return _mapper.Map<GetSaleResult>(result);
    }
}