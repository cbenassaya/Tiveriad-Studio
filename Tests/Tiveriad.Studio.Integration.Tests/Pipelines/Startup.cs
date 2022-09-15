using Microsoft.Extensions.DependencyInjection;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Generators.Net.Middlewares;
using Tiveriad.Studio.Infrastructure;
using Tiveriad.UnitTests;

namespace Tiveriad.Studio.Integration.Tests.Pipelines;

public class Startup : StartupBase
{
    public override void Configure(IServiceCollection services)
    {
        services.AddInfrastructure();
        services.AddApplication();
        services.AddScoped<NetCodeBuilderMiddleware>();
        services.AddScoped<WriterMiddleware>();
        services.AddScoped<IMiddlewareResolver, PipelineMiddlewareResolver>();
        services.AddScoped<IPipelineBuilder<PipelineModel, PipelineContext, PipelineConfiguration>, DefaultPipelineBuilder<PipelineModel, PipelineContext, PipelineConfiguration>>();
    }
}