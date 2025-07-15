using MyMediator.Application.Accounts.Repositories.Abstractions;
using MyMediator.Domain.Accounts.Entities;

namespace MyMediator.Infra.Accounts.Repositories;

public class AccountRepository : IAccountRepository
{
    public Task saveAsync(Account account, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Account {account.Id} has been saved.");
        return Task.CompletedTask;
    }
}
