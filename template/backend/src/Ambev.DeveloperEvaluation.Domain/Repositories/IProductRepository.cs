using Ambev.DeveloperEvaluation.Common.Infraestructure;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface IProductRepository : IRepository<Product>,ISearchData<Product>
{
    public Task<Product?> GetByNameAsync(Guid userId, string name, CancellationToken cancellationToken = default);
}