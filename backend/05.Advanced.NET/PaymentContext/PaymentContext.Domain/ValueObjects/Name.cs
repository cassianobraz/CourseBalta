using Flunt.Validations;
using PaymentContext.Shared.ValueObjects;
using System.Xml.Linq;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        var contract = new Contract<Name>()
            .Requires()
            .HasMinLen(FirstName, 3, "Name.FirstName", "Nome deve conter pelo menos 3 caracteres")
            .HasMinLen(LastName, 3, "Name.LastName", "Sobrenome deve conter pelo menos 3 caracteres")
            .HasMaxLen(FirstName, 40, "Name.FirstName", "Nome deve conter até 40 caracteres");

        AddNotifications(contract);
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
}
