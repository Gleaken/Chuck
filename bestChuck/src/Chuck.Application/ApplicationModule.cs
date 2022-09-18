using System.Reflection;
using Chuck.Application.Features.Filters;
using Chuck.Application.Validators;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Chuck.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddScoped<IQuoteFilter, QuoteLengthFilter>();
        services.AddScoped<IQuoteFilter, AlreadyHarvestedFilter>();
        services.AddScoped<IValidator, QuoteValidator>();
        return services;
    }
}