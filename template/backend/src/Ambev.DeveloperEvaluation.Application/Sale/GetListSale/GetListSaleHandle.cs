using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetListSale;

public class GetListSaleHandle : IRequestHandler<GetListSaleQuery, PaginatedList<GetListSaleResult>>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;

    public GetListSaleHandle(ISaleRepository saleRepository, IMapper mapper)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
    }

    public async Task<PaginatedList<GetListSaleResult>> Handle(GetListSaleQuery request,
        CancellationToken cancellationToken)
    {
        var paginatedSales = await _saleRepository.SearchAsync(request.Page ?? 1, request.Size ?? 10, request.Order ?? string.Empty, cancellationToken);
        
        var mappedSales = _mapper.Map<List<GetListSaleResult>>(paginatedSales);
        
        return new PaginatedList<GetListSaleResult>(mappedSales, paginatedSales.TotalCount, request.Page ?? 1, request.Size ?? 10);
    }
}