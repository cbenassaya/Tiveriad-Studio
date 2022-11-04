using Tiveriad.Commons.Extensions;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.Projects;
using Tiveriad.Studio.Generators.Net.Sources;
using Tiveriad.Studio.Generators.Services;
using Tiveriad.Studio.Generators.Sources;

namespace Tiveriad.Studio.Generators.Net.Middlewares;

public class FileWriterMiddleware : IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    public Task Run(PipelineContext context, PipelineModel model)
    {
        var sourceItems = context.Properties.GetOrAdd("SourceItems", ()=> new List<SourceItem>()) as IList<SourceItem>;
        var writer = new FileExportSourceItem();

        foreach (var sourceItem in sourceItems)
            writer.Export(
                sourceItem.Source.NormalizeWhitespace(),
                fileName: sourceItem.Name,
                rootDirectory: context.Configuration.OutputPath,
                pathDirectory: sourceItem.Directory,
                replaceIfExist: true
            );
        return Task.CompletedTask;
    }
}