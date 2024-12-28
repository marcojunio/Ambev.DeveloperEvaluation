using Ambev.DeveloperEvaluation.Common.Extensions;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly DefaultContext _defaultContext;

    public CompanyRepository(DefaultContext defaultContext)
    {
        _defaultContext = defaultContext;
    }

    public async Task<Company> CreateAsync(Company data, CancellationToken cancellationToken = default)
    {
        await _defaultContext.Companies.AddAsync(data, cancellationToken);
        await _defaultContext.SaveChangesAsync(cancellationToken);
        return data;
    }

    public async Task<Company?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _defaultContext.Companies.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<Company?> UpdateAsync(Company data, CancellationToken cancellationToken = default)
    {
        _defaultContext.Companies.Update(data);
        
        await _defaultContext.SaveChangesAsync(cancellationToken);
        
        return await GetByIdAsync(data.Id,cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var data = await GetByIdAsync(id, cancellationToken);
        
        if (data == null)
            return false;

        _defaultContext.Companies.Remove(data);

        await _defaultContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<Company?> GetByNameAsync(Guid userId, string name, CancellationToken cancellationToken = default)
    {
        return await _defaultContext.Companies.FirstOrDefaultAsync(c => c.UserId == userId && c.Name ==  name, cancellationToken: cancellationToken);
    }

    public async Task<bool> ExistCompanyAsync(Guid id,CancellationToken cancellationToken = default)
    {
        return await _defaultContext.Companies.AnyAsync(f => f.Id == id, cancellationToken: cancellationToken);
    }
}