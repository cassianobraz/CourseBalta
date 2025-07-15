using System.Reflection;
using Mediator.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace MyMediator.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(typeof(DependencyInjection).Assembly);
        return services;
    }
}
