using Microsoft.Extensions.DependencyInjection;
using Tiveriad.Studio.Core.Services;
using Tiveriad.Studio.Infrastructure.Services;

namespace Tiveriad.Studio.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<ILoaderService, LoaderService>();
        services.AddScoped<IParserService, XmlParserService>();
        return services;
    }
}