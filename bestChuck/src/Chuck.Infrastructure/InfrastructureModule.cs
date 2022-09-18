using Chuck.Application;
using Microsoft.Extensions.DependencyInjection;

namespace Chuck.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
    {
        services.AddSingleton<IQuotesRepository, QuotesRepository>();
        return services;
    }
}