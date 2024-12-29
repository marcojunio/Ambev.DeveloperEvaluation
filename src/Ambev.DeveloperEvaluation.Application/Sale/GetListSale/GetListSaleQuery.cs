using Ambev.DeveloperEvaluation.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Ambev.DeveloperEvaluation.Application.Sale.GetListSale;

public class GetListSaleQuery : BasePagination, IRequest<PaginatedList<GetListSaleResult>>
{
    public Guid UserId { get; set; }
    
    public GetListSaleQuery(Guid userId, int? page, int? size, string? order) : base(page, size, order)
    {
        UserId = userId;
    }
}