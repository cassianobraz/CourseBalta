namespace DependencyStore.Models;

public class Order
{
    public Order(decimal delieryFee, decimal discount, List<Product> products)
    {
        Code = Guid.NewGuid().ToString().ToUpper().Substring(0, 8);
        Date = DateTime.Now;
        DeliveryFee = delieryFee;
        Discount = discount;
    }
    public string Code { get; set; }
    public DateTime Date { get; set; }
    public decimal DeliveryFee { get; set; }
    public decimal Discount { get; set; }
    public List<Product> Products { get; set; }
    public decimal SubTotal => Products.Sum(p => p.Price);
    public decimal Total => SubTotal - Discount + DeliveryFee;
}