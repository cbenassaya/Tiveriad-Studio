using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.Projects;
using Tiveriad.Studio.Generators.Services;

namespace Tiveriad.Studio.Generators.Net.Middlewares;

public class CleanSlnMiddleware : IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private IProjectTemplateService<InternalType, ProjectDefinition> _projectTemplateService;


    public CleanSlnMiddleware(IProjectTemplateService<InternalType, ProjectDefinition> projectTemplateService)
    {
        _projectTemplateService = projectTemplateService;
    }

    public Task Run(PipelineContext context, PipelineModel model)
    {
        if (string.IsNullOrEmpty(context.Configuration.OutputPath)) throw new ArgumentNullException("OutputPath");
        if (!Directory.Exists(context.Configuration.OutputPath)) throw new DirectoryNotFoundException(context.Configuration.OutputPath);

        if (Directory.Exists(Path.Combine(context.Configuration.OutputPath, "Components")))
            Directory.Delete(Path.Combine(context.Configuration.OutputPath, "Components"), true);

        if (Directory.Exists(Path.Combine(context.Configuration.OutputPath, "Tests")))
            Directory.Delete(Path.Combine(context.Configuration.OutputPath, "Tests"), true);

        if (File.Exists(Path.Combine(context.Configuration.OutputPath, $"{model.Project.RootNamespace}.sln")))
            File.Delete(Path.Combine(context.Configuration.OutputPath, $"{model.Project.RootNamespace}.sln"));
        return Task.CompletedTask;
    }
}