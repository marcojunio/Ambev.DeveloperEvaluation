using Ambev.DeveloperEvaluation.Common.Extensions;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly DefaultContext _defaultContext;

    public CustomerRepository(DefaultContext defaultContext)
    {
        _defaultContext = defaultContext;
    }

    public async Task<Customer> CreateAsync(Customer data, CancellationToken cancellationToken = default)
    {
        await _defaultContext.Customers.AddAsync(data, cancellationToken);
        await _defaultContext.SaveChangesAsync(cancellationToken);
        return data;
    }

    public async Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _defaultContext.Customers.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
    }

    public async Task UpdateAsync(Customer data, CancellationToken cancellationToken = default)
    {
        _defaultContext.Customers.Update(data);
        await _defaultContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var data = await GetByIdAsync(id, cancellationToken);
        if (data == null)
            return false;

        _defaultContext.Customers.Remove(data);

        await _defaultContext.SaveChangesAsync(cancellationToken);

        return true;
    }

    public IQueryable<Customer> SearchAsync(string sort)
    {
        return _defaultContext.Customers
            .AsNoTracking()
            .ApplyOrdering(sort);
    }
}