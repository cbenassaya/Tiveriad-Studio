using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.Projects;
using Tiveriad.Studio.Generators.Net.Sources;
using Tiveriad.Studio.Generators.Services;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Middlewares;

public class WriterMiddleware : IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private readonly IProjectTemplateService<InternalType, ProjectDefinition> defaultProjectTemplateService;

    public WriterMiddleware(IProjectTemplateService<InternalType, ProjectDefinition> defaultProjectTemplateService)
    {
        this.defaultProjectTemplateService = defaultProjectTemplateService;
    }

    public void Run(PipelineContext context, PipelineModel model)
    {
        var sourceItems = context.Properties.SourceItems as IList<SourceItem>;
        var writer = new FileExportSourceItem();

        foreach (var sourceItem in sourceItems)
            writer.Export(
                sourceItem.Source.NormalizeWhitespace(),
                fileName: $"{sourceItem.InternalType.Name}.cs",
                rootDirectory: context.Configuration.OutputPath,
                pathDirectory: defaultProjectTemplateService.GetItemPath(sourceItem.InternalType),
                replaceIfExist: true
            );
    }
}