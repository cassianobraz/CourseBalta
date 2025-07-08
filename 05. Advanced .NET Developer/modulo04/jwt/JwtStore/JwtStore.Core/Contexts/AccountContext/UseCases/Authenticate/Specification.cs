using Flunt.Notifications;
using Flunt.Validations;

namespace JwtStore.Core.Contexts.AccountContext.UseCases.Authenticate;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request) =>
        new Contract<Notification>()
            .Requires()
            .IsLowerThan(request.Password.Length, 40, "Password", "The password than menus fourty caracter")
            .IsGreaterThan(request.Password.Length, 8, "Password", "The password than more eight caracter")
            .IsEmail(request.Email, "Email", "The email is not valid");
}