using CleanStore.Application.AccountContext.Repositories.Abstractions;
using CleanStore.Application.SharedContext.Repositories.Abstractions;
using CleanStore.Application.SharedContext.Results;
using CleanStore.Application.SharedContext.UseCases.Abstractions;
using CleanStore.Domain.AccountContext.Entities;
using CleanStore.Domain.AccountContext.ValueObjects;

namespace CleanStore.Application.AccountContext.UseCases.Create;

public sealed class Handler(IAccountRepository accountRepository, IUnitOfWork unitOfWork) : ICommandHandler<Command, Response>
{
    public async Task<Result<Response>> Handle(Command request, CancellationToken cancellationToken)
    {
        #region Verificar E-mail cadastrado
        var emailExists = await accountRepository.VerifyEmailExistsAsync(request.Email);

        if (emailExists)
            return Result.Failure<Response>(new Error("400", "Exists e-mail."));
        #endregion

        #region Vos

        var email = Email.Create(request.Email);

        #endregion

        #region entidade

        var account = Account.Create(email);

        #endregion

        #region Persistir dados

        await accountRepository.SaveAsync(account);
        await unitOfWork.CommitAsync();

        #endregion


        #region Resposta

        var response = new Response(account.Id, account.Email);
        return Result.Success<Response>(response);

        #endregion
    }
}
