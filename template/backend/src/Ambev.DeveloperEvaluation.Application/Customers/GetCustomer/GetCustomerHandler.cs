using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Customers.GetCustomer;

public class GetCustomerHandler : IRequestHandler<GetCustomerQuery, GetCustomerResult>
{
    private readonly ICustomerRepository _companyRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetCustomerHandler(
        ICustomerRepository companyRepository, IMapper mapper, ICacheService cacheService)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<GetCustomerResult> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetCustomerValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = await _cacheService.GetOrCreateAsync(CacheKeys.GetCustomerKey(request.Id),async () => await _companyRepository.GetByIdAsync(request.Id, cancellationToken));

        if (result == null)
            throw new NotFoundException($"Customer with ID {request.Id} not found");

        return _mapper.Map<GetCustomerResult>(result);
    }
}