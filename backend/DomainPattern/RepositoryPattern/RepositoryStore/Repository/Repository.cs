using Microsoft.EntityFrameworkCore;
using RepositoryStore.Repository.Abstractions;

namespace RepositoryStore.Repository;

public abstract class Repository<T>(DbContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<T> CreateAsynt(T entity, CancellationToken ct = default)
    {
        await _dbSet.AddAsync(entity, ct);
        await context.SaveChangesAsync(ct);
        return entity;
    }

    public async Task<T> UpdateAsync(T entity, CancellationToken ct = default)
    {
        _dbSet.Update(entity);
        await context.SaveChangesAsync(ct);

        return entity;
    }

    public async Task<T> DeleteAsync(T entity, CancellationToken ct = default)
    {
        _dbSet.Remove(entity);
        await context.SaveChangesAsync(ct);

        return entity;
    }

    public async Task<T?> GetByIdAsync(int id, CancellationToken ct = default)
        => await _dbSet.FindAsync(id, ct);

    public async Task<List<T>?> GetAllAsync(int skip = 0, int take = 25, CancellationToken ct = default)
    => await _dbSet.AsNoTracking().Skip(skip).Take(take).ToListAsync(ct);
}
