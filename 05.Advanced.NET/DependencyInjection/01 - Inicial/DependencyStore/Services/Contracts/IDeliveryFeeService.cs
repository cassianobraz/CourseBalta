namespace DependencyStore.Services.Contracts;

public interface IDeliveryFeeService
{
    Task<decimal> GetDeliveruFeeAsync(string zipCode);
}
