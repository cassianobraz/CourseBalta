namespace CassianoStore.Domain.Abstractions;

public interface IUnitOfWork
{
    Task CommitAsync();
}
