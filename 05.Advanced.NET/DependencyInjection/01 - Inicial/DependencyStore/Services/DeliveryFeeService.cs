﻿using DependencyStore.Services.Contracts;
using RestSharp;
using System.Reflection.Emit;

namespace DependencyStore.Services;

public class DeliveryFeeService : IDeliveryFeeService
{
    private readonly Configuration _configuration;

    public DeliveryFeeService(Configuration configuration)
        => _configuration = configuration;

    public async Task<decimal> GetDeliveruFeeAsync(string zipCode)
    {
        var client = new RestClient(_configuration.DeliveryFeeServiceUrl);

        var request = new RestRequest()
                .AddJsonBody(new
                {
                    zipCode
                });
        var response = await client.PostAsync<decimal>(request);
        return response < 5 ? 5 : response;
    }
}
