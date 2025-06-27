using DependencyStore.Services.Contracts;
using RestSharp;
using System.Reflection.Emit;

namespace DependencyStore.Services;

public class DeliveryFeeService : IDeliveryFeeService
{
    public async Task<decimal> GetDeliveruFeeAsync(string zipCode)
    {
        var client = new RestClient("https://consultafrete.io/cep/");

        var request = new RestRequest()
                .AddJsonBody(new
                {
                    zipCode
                });
        var response = await client.PostAsync<decimal>(request);
        return response < 5 ? 5 : response;
    }
}
