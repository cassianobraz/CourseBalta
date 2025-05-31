namespace PaymentContext.Domain.Entities;

public class PayPalPayment : Payment
{
    public PayPalPayment(string transactionCode)
    {
        TransactionCode = transactionCode;
    }

    public string TransactionCode { get; private set; }
}