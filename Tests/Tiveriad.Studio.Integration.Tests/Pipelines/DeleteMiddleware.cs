using System.Xml.Serialization;
using Optional.Unsafe;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Net.Middlewares;
using Tiveriad.Studio.Generators.Projects;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Integration.Tests.Pipelines;

public class DeleteMiddleware : IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    public void Run(PipelineContext context, PipelineModel model)
    {
        var sourceItems = context.Properties.SourceItems as IList<SourceItem>;
        var assembly = typeof(WriterMiddleware).Assembly;
        var xmlSerializer = new XmlSerializer(typeof(ProjectTemplate));
        using var stream =
            assembly.GetManifestResourceStream("Tiveriad.Studio.Generators.Net.Projects.ProjectTemplate.xml");
        var projectTemplate = xmlSerializer.Deserialize(stream) as ProjectTemplate;
        var defaultProjectTemplateService = new DefaultProjectTemplateService(projectTemplate);


        foreach (var sourceItem in sourceItems)
        {
            var path = Path.Combine(context.Configuration.OutputPath,
                defaultProjectTemplateService.GetPath(sourceItem.InternalType, model.Project),
                $"{sourceItem.InternalType.Name.ValueOrFailure()}.cs");
            if (File.Exists(path))
                File.Delete(path);
        }

    }

}