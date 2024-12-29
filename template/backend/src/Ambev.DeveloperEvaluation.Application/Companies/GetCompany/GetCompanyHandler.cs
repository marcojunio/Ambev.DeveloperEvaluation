using Ambev.DeveloperEvaluation.Common.Cache;
using Ambev.DeveloperEvaluation.Domain.Exceptions;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Companies.GetCompany;

public class GetCompanyHandler : IRequestHandler<GetCompanyQuery, GetCompanyResult>
{
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;
    private readonly ICacheService _cacheService;

    public GetCompanyHandler(
        ICompanyRepository companyRepository, IMapper mapper, ICacheService cacheService)
    {
        _companyRepository = companyRepository;
        _mapper = mapper;
        _cacheService = cacheService;
    }

    public async Task<GetCompanyResult> Handle(GetCompanyQuery request, CancellationToken cancellationToken)
    {
        var validator = new GetCompanyValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var result = await _cacheService.GetOrCreateAsync(CacheKeys.GetCompanyKey(request.Id),async () => await _companyRepository.GetByIdAsync(request.Id, cancellationToken));

        if (result == null)
            throw new NotFoundException($"Company with ID {request.Id} not found");

        return _mapper.Map<GetCompanyResult>(result);
    }
}