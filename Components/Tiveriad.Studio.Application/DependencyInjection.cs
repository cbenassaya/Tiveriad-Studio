using Microsoft.Extensions.DependencyInjection;
using Tiveriad.Studio.Application.Middlewares;
using Tiveriad.Studio.Application.Services;
using Tiveriad.Studio.Core.Services;

namespace Tiveriad.Studio.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IXTypeService, XTypeService>();
        services.AddScoped<LoadingMiddleware>();
        services.AddScoped<AddTypesMiddleware>();
        services.AddScoped<PostLoadingMiddleware>();
        services.AddScoped<ContextBuilderMiddleware>();
        services.AddScoped<InjectorMiddleware>();
        services.AddScoped<ManyToManyMiddleware>();
        services.AddScoped<QueryMiddleware>();
        services.AddScoped<CommandMiddleware>();
        services.AddScoped<EndpointMiddleware>();
        services.AddScoped<AuditableMiddleware>();
        services.AddScoped<MultiTenancyMiddleware>();
        return services;
    }
}