using System.Linq.Expressions;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Common.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T>? query, string orderBy)
    {
        if (string.IsNullOrEmpty(orderBy))
            return query!;

        var orders = orderBy.Split(',').Select(o => o.Trim());
        bool isFirst = true;

        foreach (var order in orders)
        {
            var orderParts = order.Split(' ');

            if (orderParts.Length != 2 ||
                (orderParts[1].ToLower() != "asc" && orderParts[1].ToLower() != "desc"))
                throw new ArgumentException($"Invalid order format: {order}");

            var propertyName = orderParts[0];
            var ascending = orderParts[1].ToLower() == "asc";

            query = ApplyOrder(query, propertyName, ascending, isFirst);
            isFirst = false;
        }

        return query!;
    }

    private static IQueryable<T> ApplyOrder<T>(IQueryable<T>? query, string propertyPath, bool ascending, bool isFirst)
    {
        var parameter = Expression.Parameter(typeof(T), "x");
        
        var property = propertyPath.Split('.').Aggregate<string?, Expression>(parameter, Expression.PropertyOrField!);

        var keySelector = Expression.Lambda(property, parameter);

        var methodName = isFirst
            ? ascending ? "OrderBy" : "OrderByDescending"
            : ascending
                ? "ThenBy"
                : "ThenByDescending";

        var method = typeof(Queryable).GetMethods()
            .First(m => m.Name == methodName && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(T), property.Type);

        return (IQueryable<T>)method.Invoke(null, new object[] { query, keySelector });
    }

    public static async Task<PaginatedList<T>> PaginateAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize,
        CancellationToken cancellationToken = default)
    {
        var count = await source.CountAsync(cancellationToken: cancellationToken);
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
}