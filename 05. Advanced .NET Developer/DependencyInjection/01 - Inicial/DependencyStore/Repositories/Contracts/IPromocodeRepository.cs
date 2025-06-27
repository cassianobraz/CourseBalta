using DependencyStore.Models;

namespace DependencyStore.Repositories.Contracts;

public interface IPromocodeRepository
{
    Task<PromoCode?> GetPromoCodeAsync(string promoCode);
}
