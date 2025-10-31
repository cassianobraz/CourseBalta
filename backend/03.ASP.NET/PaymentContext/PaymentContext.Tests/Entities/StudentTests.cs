using PaymentContext.Domain.Entities;

namespace PaymentContext.Tests.Entities;

[TestClass]
public class StudentTests
{
    [TestMethod]
    public void AdicionarAssinatura()
    {
        var subscription = new Subscription();
        var student = new Student("Cassiano", "Braz", "12345678900", "cassiano@teste.com");
        student.AddSubscription(subscription);
    }
}
