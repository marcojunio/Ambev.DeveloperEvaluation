using Ambev.DeveloperEvaluation.Common.Extensions;
using Ambev.DeveloperEvaluation.Common.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class SaleRepository : ISaleRepository
{
    private readonly DefaultContext _defaultContext;

    public SaleRepository(DefaultContext defaultContext)
    {
        _defaultContext = defaultContext;
    }

    public async Task<Sale> CreateAsync(Sale data, CancellationToken cancellationToken = default)
    {
        await using var transaction = await _defaultContext.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            await _defaultContext.Sales.AddAsync(data, cancellationToken);

            foreach (var item in data.Items)
            {
                item.SaleId = data.Id;

                await _defaultContext.SaleItems.AddAsync(item, cancellationToken);
            }

            await _defaultContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return data;
        }
        catch (Exception)
        {
            await transaction.RollbackAsync(cancellationToken);

            throw;
        }
    }

    public async Task<Sale?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _defaultContext.Sales
            .Include(s => s.Items)
            .FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task<Sale?> UpdateAsync(Sale data, CancellationToken cancellationToken = default)
    {
        _defaultContext.Sales.Update(data);

        await _defaultContext.SaveChangesAsync(cancellationToken);

        return await GetByIdAsync(data.Id, cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var data = await GetByIdAsync(id, cancellationToken);
        if (data == null)
            return false;

        _defaultContext.Sales.Remove(data);

        await _defaultContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public async Task<PaginatedList<Sale>> SearchAsync(Guid userId,int pageNumber, int pageSize, string order,
        CancellationToken cancellationToken = default)
    {
        return await _defaultContext.Sales
            .Where(s => s.UserId == userId)
            .Include(f => f.Items)
            .AsSplitQuery()
            .AsNoTracking()
            .ApplyOrdering(order)
            .PaginateAsync(pageNumber, pageSize, cancellationToken: cancellationToken);
    }
}