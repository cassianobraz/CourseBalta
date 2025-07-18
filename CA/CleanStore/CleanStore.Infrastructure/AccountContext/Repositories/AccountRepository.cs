using CleanStore.Application.AccountContext.Repositories.Abstractions;
using CleanStore.Domain.AccountContext.Entities;
using CleanStore.Infrastructure.SharedContext.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanStore.Infrastructure.AccountContext.Repositories;

public class AccountRepository(AppDbContext context) : IAccountRepository
{
    public async Task<bool> VerifyEmailExistsAsync(string email)
        => await context.Accounts.AsNoTracking().AnyAsync(e => e.Email == email);

    public async Task SaveAsync(Account account)
        => await context.Accounts.AddAsync(account);
}
