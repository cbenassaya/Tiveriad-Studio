using System.Xml.Serialization;
using Microsoft.Extensions.DependencyInjection;
using Tiveriad.Commons.Extensions;
using Tiveriad.Pipelines;
using Tiveriad.Pipelines.DependencyInjection;
using Tiveriad.Studio.Application;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.Middlewares;
using Tiveriad.Studio.Generators.Net.Projects;
using Tiveriad.Studio.Generators.Net.Transformers;
using Tiveriad.Studio.Generators.Projects;
using Tiveriad.Studio.Generators.Services;
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
        services.AddScoped<NetXTypeToInternalTypeMiddleware>();
        services.AddScoped<LinkerAndBuilderMiddleware>();
        services.AddScoped<CleanSlnMiddleware>();
        services.AddScoped<CreateSlnMiddleware>();
        services.AddScoped<WriterMiddleware>();
        services.AddScoped<IServiceResolver, DependencyInjectionServiceResolver>();
        services
            .AddScoped<IPipelineBuilder<PipelineModel, PipelineContext, PipelineConfiguration>,
                DefaultPipelineBuilder<PipelineModel, PipelineContext, PipelineConfiguration>>();

        TemplateRendererFactoryBuilder
            .With<ScribanTemplateRenderer, ScribanTemplateRendererConfiguration>()
            .Add(typeof(ActionBuilderRequest).Assembly)
            .Configure(configuration =>
            {
                configuration.Add(typeof(StringExtensions));
                configuration.Add(typeof(XClassifierExtensions));
                configuration.Add(typeof(XEntityExtensions));
            })
            .Register(renderer => { services.AddSingleton<ITemplateRenderer>(renderer); });


        var assembly = typeof(WriterMiddleware).Assembly;
        var xmlSerializer = new XmlSerializer(typeof(ProjectDefinitionTemplate));
        using var stream =
            assembly.GetManifestResourceStream("Tiveriad.Studio.Generators.Net.Projects.ProjectDefinitionTemplate.xml");
        var projectTemplate = (ProjectDefinitionTemplate)xmlSerializer.Deserialize(stream) ??
                              new ProjectDefinitionTemplate();
        services.AddSingleton<IProjectTemplateService<InternalType, ProjectDefinition>>(
            new NetProjectTemplateService(projectTemplate));
    }
}