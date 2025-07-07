using JwtStore.Core.AccountContext.ValueObjects;
using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Create.Contracts;
using JwtStore.Core.Contexts.AccountContext.ValueObjects;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Create;

public class Handler
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(IRepository repository, IService service)
    {
        _repository = repository;
        _service = service;
    }
    public async Task<Response> Handler(Request request, CancellationToken cancellationToken)
    {
        #region 01. Valida a requisição
        try
        {
            var res = Specification.Ensure(request);
            if (!res.IsValid)
                return new Response("Request invalid!", 400, res.Notifications);
        }
        catch
        {
            return new Response("Not possible valid your request!", 500);
        }
        #endregion

        #region 02. Gerar os objetos
        Email email;
        Password password;
        User user;

        try
        {
            email = new Email(request.Email);
            password = new Password(request.Password);
            user = new User(request.Name, email, password);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }
        #endregion

        #region 03. Verificar se o Usuário
        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);
            if (exists)
                return new Response("this E-mail there is using", 400);
        }
        catch
        {
            return new Response("Fail a verify E-mail cadastred", 500);
        }
        #endregion

        #region 04. Persistir os Dados
        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            return new Response("Fail a persist data", 500);
        }
        #endregion

        #region 05. Enviar E-mail de ativação
        try
        {
            await _service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch (Exception)
        {
            // Do nothing
        }
        #endregion

        return new Response("Create acount", new ResponseData(user.Id, user.Name, user.Email));
    }
}