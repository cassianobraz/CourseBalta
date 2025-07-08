using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;

namespace PaymentContext.Domain.ValueObjects;

public class Email : ValueObject
{
    public Email(string address)
    {
        Address = address;

        var contract = new Contract<Email>()
            .Requires()
            .IsEmail(Address, "Email.Address", "E-mail inválido");

        AddNotifications(contract);
    }

    public string Address { get; private set; }
}
