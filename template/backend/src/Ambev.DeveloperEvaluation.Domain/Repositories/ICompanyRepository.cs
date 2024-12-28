using Ambev.DeveloperEvaluation.Common.Infraestructure;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Domain.Repositories;

public interface ICompanyRepository : IRepository<Company>
{
    Task<Company?> GetByNameAsync(Guid userId,string name, CancellationToken cancellationToken = default);
    
    public Task<bool> ExistCompanyAsync(Guid id,CancellationToken cancellationToken = default);
}