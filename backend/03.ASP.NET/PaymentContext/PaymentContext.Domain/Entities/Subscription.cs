namespace PaymentContext.Domain.Entities;

public class Subscription
{
    private readonly IList<Payment> _payments;
    public Subscription(DateTime? expireDate)
    {
        CreateDate = DateTime.Now;
        LastUpdateDate = DateTime.Now;
        ExpireDate = expireDate;
        Active = true;
        Payments = new List<Payment>();
    }

    public DateTime CreateDate { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public DateTime? ExpireDate { get; set; }
    public bool Active { get; set; }
    public List<Payment> Payments { get; set; }
}

//minuto 19:30