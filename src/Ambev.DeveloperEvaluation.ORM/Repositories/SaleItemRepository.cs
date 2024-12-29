using Ambev.DeveloperEvaluation.Common.Extensions;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleItemRepository : ISaleItemRepository
{
    private readonly DefaultContext _defaultContext;

    public SaleItemRepository(DefaultContext defaultContext)
    {
        _defaultContext = defaultContext;
    }

    public async Task<SaleItem> CreateAsync(SaleItem data, CancellationToken cancellationToken = default)
    {
        await _defaultContext.SaleItems.AddAsync(data, cancellationToken);
        await _defaultContext.SaveChangesAsync(cancellationToken);
        return data;
    }

    public async Task<SaleItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _defaultContext.SaleItems.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<SaleItem?> UpdateAsync(SaleItem data, CancellationToken cancellationToken = default)
    {
        _defaultContext.SaleItems.Update(data);
        
        await _defaultContext.SaveChangesAsync(cancellationToken);
        
        return await GetByIdAsync(data.Id,cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var data = await GetByIdAsync(id, cancellationToken);
        if (data == null)
            return false;

        _defaultContext.SaleItems.Remove(data);

        await _defaultContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}