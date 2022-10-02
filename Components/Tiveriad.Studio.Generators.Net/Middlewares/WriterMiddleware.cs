using System.Xml.Serialization;
using Optional.Unsafe;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Net.Sources;
using Tiveriad.Studio.Generators.Projects;
using Tiveriad.Studio.Generators.Services;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Middlewares;

public class WriterMiddleware : IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
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
        var writer = new FileExportSourceItem();

        foreach (var sourceItem in sourceItems)
            writer.Export(
                sourceItem.Source.NormalizeWhitespace(),
                fileName: $"{sourceItem.InternalType.Name.ValueOrFailure()}.cs",
                rootDirectory: context.Configuration.OutputPath,
                pathDirectory: defaultProjectTemplateService.GetPath(sourceItem.InternalType, model.Project),
                replaceIfExist: true
            );
    }
}