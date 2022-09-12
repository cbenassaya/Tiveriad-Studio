using System.ComponentModel.Design.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application;
using Tiveriad.Studio.Application.Middlewares;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Services;
using Tiveriad.Studio.Infrastructure;
using Tiveriad.Studio.Infrastructure.Services;
using Tiveriad.UnitTests;
using Xunit;

namespace Tiveriad.Studio.Integration.Tests.Pipelines;


public class PipelineMiddlewareResolver : IMiddlewareResolver
{
    private readonly IServiceProvider _serviceProvider;
    public object Resolve(Type type)
    {
        return _serviceProvider.GetRequiredService(type);
    }
}

public class Startup : StartupBase
{
    public override void Configure(IServiceCollection services)
    {
        services.AddInfrastructure();
        services.AddApplication();
        services.AddScoped<IMiddlewareResolver, PipelineMiddlewareResolver>();
        services.AddScoped<IPipelineBuilder<PipelineModel, PipelineContext, PipelineConfiguration>, DefaultPipelineBuilder<PipelineModel, PipelineContext, PipelineConfiguration>>();
    }
}


public class PipelineModule: TestBase<Startup>
{
    [Fact]
    public void Integration_Test()
    {
        var pipelineBuilder = GetRequiredService<IPipelineBuilder<PipelineModel, PipelineContext, PipelineConfiguration>>();
        pipelineBuilder
            .Add<LoadingMiddleware>()
            .Add<AddTypesMiddleware>()
            .Add<PostLoadingMiddleware>()
            .Add<ContextBuilderMiddleware>()
            .Add<InjectorMiddleware>()
            .Add<ManyToManyMiddleware>();

        var pipeline = pipelineBuilder.Build();
        pipeline.Execute(new PipelineModel(){ InputPath = "Samples/KpiBuilder.xml"});
    }
}