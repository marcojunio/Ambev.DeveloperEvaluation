namespace Ambev.DeveloperEvaluation.Common.Pagination;

public class PaginatedList<T> : List<T>
{
    public string? Order { get; set; }

    public int? PageSize { get; private set; }
    public int CurrentPage { get; private set; }
    public int TotalCount { get; private set; }
    public int TotalPages { get; private set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PaginatedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
    {
        TotalCount = count;
        PageSize = pageSize;
        CurrentPage = pageNumber;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);

        AddRange(items);
    }
}

public abstract class BasePagination
{
    protected BasePagination(int? page, int? size, string? order)
    {
        Page = page ?? 1;
        Size = size ?? 10;
        Order = order ?? string.Empty;
    }
    public int? Page { get; set; }
    public int? Size { get; set; }
    public string? Order { get; set; }
}