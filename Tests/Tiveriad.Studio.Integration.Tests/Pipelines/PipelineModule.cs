using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Middlewares;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Generators.Net.Middlewares;
using Tiveriad.UnitTests;
using Xunit;

namespace Tiveriad.Studio.Integration.Tests.Pipelines;

public class PipelineModule : TestBase<Startup>
{
    [Fact]
    public void Integration_Test()
    {
        var pipelineBuilder =
            GetRequiredService<IPipelineBuilder<PipelineModel, PipelineContext, PipelineConfiguration>>();
        pipelineBuilder
            .Configure(x => x.OutputPath = @"C:\Dev\Data\Source")
            .Add<LoadingMiddleware>()
            .Add<AddTypesMiddleware>()
            .Add<PostLoadingMiddleware>()
            .Add<ContextBuilderMiddleware>()
            .Add<InjectorMiddleware>()
            .Add<ManyToManyMiddleware>()
            .Add<QueryMiddleware>()
            .Add<CommandMiddleware>()
            .Add<EndpointMiddleware>()
            .Add<NetCodeBuilderMiddleware>()
            //.Add<DeleteMiddleware>()
            .Add<WriterMiddleware>();

        var pipeline = pipelineBuilder.Build();
        pipeline.Execute(new PipelineModel { InputPath = "Samples/KpiBuilder.xml" });
    }
}