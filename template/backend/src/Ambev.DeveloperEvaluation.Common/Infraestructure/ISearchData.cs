namespace Ambev.DeveloperEvaluation.Common.Infraestructure;

public interface ISearchData<T>
{
    IQueryable<T> SearchAsync(string sort);
}