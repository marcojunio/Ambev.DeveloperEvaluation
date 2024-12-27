using Ambev.DeveloperEvaluation.Common.Infraestructure;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICompanyRepository : IRepository<Company> , ISearchData<Company>
{
    Task<Company?> GetByNameAsync(Guid userId,string name, CancellationToken cancellationToken = default);
}