namespace Ambev.DeveloperEvaluation.WebApi.Common;

public class PaginatedResponse<T> : ApiResponse
{
    public T? Data { get; set; }
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int TotalCount { get; set; }
}