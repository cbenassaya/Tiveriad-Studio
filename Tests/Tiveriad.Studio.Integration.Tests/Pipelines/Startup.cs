using Microsoft.Extensions.DependencyInjection;
using Tiveriad.Commons.Extensions;
using Tiveriad.Pipelines;
using Tiveriad.Pipelines.DependencyInjection;
using Tiveriad.Studio.Application;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Generators.Net.Middlewares;
using Tiveriad.Studio.Generators.Net.Transformers;
using Tiveriad.Studio.Infrastructure;
using Tiveriad.TextTemplating;
using Tiveriad.TextTemplating.Scriban;
using Tiveriad.UnitTests;

namespace Tiveriad.Studio.Integration.Tests.Pipelines;

public class Startup : StartupBase
{
    public override void Configure(IServiceCollection services)
    {
        services.AddInfrastructure();
        services.AddApplication();
        services.AddTiveriadSender(typeof(ActionBuilderRequest).Assembly);
        services.AddScoped<NetCodeBuilderMiddleware>();
        services.AddScoped<WriterMiddleware>();
        services.AddScoped<IServiceResolver, DependencyInjectionServiceResolver>();
        services.AddScoped<IPipelineBuilder<PipelineModel, PipelineContext, PipelineConfiguration>, DefaultPipelineBuilder<PipelineModel, PipelineContext, PipelineConfiguration>>();
        
        TemplateRendererFactoryBuilder
            .With<ScribanTemplateRenderer, ScribanTemplateRendererConfiguration>()
            .Add(typeof(ActionBuilderRequest).Assembly)
            .Configure(configuration =>
            {
                configuration.Add(typeof(StringExtensions));
                configuration.Add(typeof(XClassifierExtensions));
                configuration.Add(typeof(XEntityExtensions));
            })
            .Register(renderer =>
            {
                services.AddSingleton<ITemplateRenderer>(renderer);
            });
    }
}