using MyMediator.Domain.Accounts.Entities;

namespace MyMediator.Application.Accounts.Repositories.Abstractions;

public interface IAccountRepository
{
    Task saveAsync(Account account, CancellationToken cancellationToken = default);
}
