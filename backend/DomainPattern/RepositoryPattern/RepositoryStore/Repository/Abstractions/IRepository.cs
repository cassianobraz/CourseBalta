using RepositoryStore.Models;

namespace RepositoryStore.Repository.Abstractions;

public interface IRepository<T> where T : class
{
    Task<T> CreateAsynt(T entity, CancellationToken ct = default);
    Task<T> UpdateAsync(T entity, CancellationToken ct = default);
    Task<T> DeleteAsync(T entity, CancellationToken ct = default);
    Task<T?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<List<T>?> GetAllAsync(int skip = 0, int take = 25, CancellationToken ct = default);
}
