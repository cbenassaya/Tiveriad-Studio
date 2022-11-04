using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
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
    public void Pipeline_WithAutoGeneration()
    {
        var pipelineBuilder =
            GetRequiredService<IPipelineBuilder<PipelineModel, PipelineContext, PipelineConfiguration>>();
        pipelineBuilder
            .Configure(x =>
            {
                x.OutputPath = @"C:\Dev\Data\Source\IdentityServer";
                x.InputPath = "Samples/IdentityServer.xml";
            })
            .WithExceptionHandler(exception =>
            {
                var logger = GetRequiredService<ILogger<PipelineModule>>();
                logger.LogError(exception.Message);
            })
            .Add<LoadingMiddleware>()
            .Add<AddTypesMiddleware>()
            .Add<PostLoadingMiddleware>()
            .Add<ContextBuilderMiddleware>()
            .Add<InjectorMiddleware>()
            .Add<ManyToManyMiddleware>()
            .Add<AuditableMiddleware>()
            .Add<MultiTenancyMiddleware>()
            .Add<QueryMiddleware>()
            .Add<CommandMiddleware>()
            .Add<EndpointMiddleware>()
            .Add<TransformerMiddleware>()
            .Add<CleanSlnMiddleware>()
            .Add<CreateSlnMiddleware>()
            .Add<LinkerAndBuilderMiddleware>()
            .Add<ProfileMiddleware>()
            .Add<ProjectFileMiddleware>()
            .Add<FileWriterMiddleware>();
            
        var pipeline = pipelineBuilder.Build();
        pipeline.Execute(new PipelineModel());
    }
    
    [Fact]
    public void Pipeline_WithoutAutoGeneration()
    {
        var pipelineBuilder =
            GetRequiredService<IPipelineBuilder<PipelineModel, PipelineContext, PipelineConfiguration>>();
        pipelineBuilder
            .Configure(x =>
            {
                x.OutputPath = @"C:\Dev\Data\Source";
                x.InputPath = "Samples/Sample.xml";
            })
            .Add<LoadingMiddleware>()
            .Add<AddTypesMiddleware>()
            .Add<PostLoadingMiddleware>()
            .Add<ContextBuilderMiddleware>()
            .Add<InjectorMiddleware>()
            .Add<AuditableMiddleware>()
            .Add<TransformerMiddleware>()
            .Add<CleanSlnMiddleware>()
            .Add<CreateSlnMiddleware>()
            .Add<LinkerAndBuilderMiddleware>()
            .Add<ProjectFileMiddleware>()
            .Add<FileWriterMiddleware>();

        var pipeline = pipelineBuilder.Build();
        pipeline.Execute(new PipelineModel());
    }
}