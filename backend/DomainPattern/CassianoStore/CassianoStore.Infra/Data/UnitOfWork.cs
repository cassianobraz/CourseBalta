using CassianoStore.Domain.Abstractions;

namespace CassianoStore.Infra.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public async Task CommitAsync()
        => await context.SaveChangesAsync();
}
