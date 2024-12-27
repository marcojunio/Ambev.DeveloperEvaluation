namespace Ambev.DeveloperEvaluation.Common.Infraestructure;

public interface IRepository<T>
{
    Task<T> CreateAsync(T data, CancellationToken cancellationToken = default);
  
    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task UpdateAsync(T data,CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}