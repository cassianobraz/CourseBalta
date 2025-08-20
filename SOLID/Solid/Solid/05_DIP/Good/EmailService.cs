namespace Solid._05_DIP.Good;

public interface IEmailService
{
    void Send();
}
public class EmailService : IEmailService
{
    public void Send()
    {
        Console.WriteLine("Sending email");
    }
}

public class FakeEmailService : IEmailService
{
    public void Send()
    {
        Console.WriteLine("FAKE");
    }
}

public class UserService(IEmailService emailService)
{

}