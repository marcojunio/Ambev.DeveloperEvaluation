using Ambev.DeveloperEvaluation.Common.Pagination;

namespace Ambev.DeveloperEvaluation.Common.Infraestructure;

public interface ISearchData<T>
{
    Task<PaginatedList<T>> SearchAsync(int pageNumber, int pageSize, string order,
        CancellationToken cancellationToken = default);
}