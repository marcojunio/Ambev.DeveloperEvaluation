using Ambev.DeveloperEvaluation.Common.Pagination;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetListSale;

public class GetListSaleRequest : BasePagination
{
    public GetListSaleRequest(int? page, int? size, string? order) : base(page, size, order)
    {
        
    }
}