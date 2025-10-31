using CassianoStore.Domain.Abstractions;
using CassianoStore.Domain.Repositories;
using CassianoStore.Infra.Data;
using CassianoStore.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace CassianoStore.Infra;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<IProductRepository, ProductRepository>();
        return services;
    }
}
