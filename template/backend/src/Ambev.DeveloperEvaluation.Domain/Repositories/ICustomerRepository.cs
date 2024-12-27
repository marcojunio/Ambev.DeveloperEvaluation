using Ambev.DeveloperEvaluation.Common.Infraestructure;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICustomerRepository : IRepository<Customer>, ISearchData<Customer>
{
    public Task<Customer?> GetByNameAsync(Guid userId, string name, CancellationToken cancellationToken = default);
}