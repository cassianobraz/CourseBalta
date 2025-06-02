using PaymentContext.Shared.ValueObjects;
using System.Xml.Linq;

namespace PaymentContext.Domain.ValueObjects;

public class Name : ValueObject
{
    public Name(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;

        if (string.IsNullOrEmpty(FirstName))
            AddNotification("Nme.FirstName", "Name invalid");
    }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
}
