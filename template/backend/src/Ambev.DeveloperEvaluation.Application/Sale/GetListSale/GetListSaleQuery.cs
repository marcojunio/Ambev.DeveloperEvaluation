using Ambev.DeveloperEvaluation.Common.Pagination;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetListSale;

public class GetListSaleQuery : BasePagination, IRequest<PaginatedList<GetListSaleResult>>
{
    public GetListSaleQuery(int? page,int? size, string? order) : base(page,size,order)
    {
        
    }
}