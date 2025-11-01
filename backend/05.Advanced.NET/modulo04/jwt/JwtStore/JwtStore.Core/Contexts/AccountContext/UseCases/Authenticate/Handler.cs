using JwtStore.Core.Contexts.AccountContext.Entities;
using JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate.Contracts;
using MediatR;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository) => _repository = repository;

    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Valid request

        try
        {
            var res = Specification.Ensure(request);
            if (!res.IsValid)
                return new Response("Request invalid", 400, res.Notifications);

        }
        catch (Exception)
        {
            return new Response("Not possible valid your request", 500);
        }

        #endregion

        #region 02. Recover profile

        User? user;
        try
        {
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);
            if (user is null)
                return new Response("Profile not found", 404);
        }
        catch (Exception ex)
        {
            return new Response("Not possible recover your profile", 500);
        }

        #endregion

        #region 03. Valid password

        if (!user.Password.Challenge(request.Password))
            return new Response("User or password invalid", 400);
        #endregion

        #region 04. Password Check is valid

        try
        {
            if (!user.Email.Verification.IsActive)
                return new Response("Acount inactive", 400);
        }
        catch (Exception)
        {
            return new Response("Not possible verify your profile", 500);
        }

        #endregion

        #region 05. Return data

        try
        {
            var data = new ResponseData
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Email = user.Email,
                Roles = user.Roles.Select(x => x.Name).ToArray()
            };
            return new Response(string.Empty, data);
        }
        catch
        {
            return new Response("Not possible get data the profile", 500);
        }

        #endregion
    }
}
