using Tiveriad.Commons.Diagnostics;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.Projects;
using Tiveriad.Studio.Generators.Services;

namespace Tiveriad.Studio.Generators.Net.Middlewares;

public class CreateSlnMiddleware : IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    private readonly IProjectTemplateService<InternalType, ProjectDefinition> _projectTemplateService;


    public CreateSlnMiddleware(IProjectTemplateService<InternalType, ProjectDefinition> projectTemplateService)
    {
        _projectTemplateService = projectTemplateService;
    }

    public void Run(PipelineContext context, PipelineModel model)
    {
        //Create Solution
        ProcessCommand.Create("dotnet", $"new sln -n {model.Project.RootNamespace}")
            .InWorkingDirectory(context.Configuration.OutputPath)
            .Execute();

        //Create Projects
        foreach (var project in _projectTemplateService.GetProjects(model.Project))
        {
            ProcessCommand.Create("dotnet ",
                    $"new {project.ProjectTemplate} -o \"{project.ProjectPath}\" -n \"{project.ProjectName}\"")
                .InWorkingDirectory(context.Configuration.OutputPath)
                .Execute();

            foreach (var dependency in _projectTemplateService.GetDependencies(project))
                ProcessCommand.Create("dotnet ", $"add package {dependency.Include} --version {dependency.Version}")
                    .InWorkingDirectory(Path.Combine(context.Configuration.OutputPath, project.ProjectPath))
                    .Execute();

            if (File.Exists(Path.Combine(context.Configuration.OutputPath, project.ProjectPath, "Class1.cs")))
                File.Delete(Path.Combine(context.Configuration.OutputPath, project.ProjectPath, "Class1.cs"));
        }

        //Link Projects
        foreach (var project in _projectTemplateService.GetProjects(model.Project))
            ProcessCommand.Create("dotnet ",
                    $"sln \"{model.Project.RootNamespace}.sln\"  add  \"{Path.Combine(project.ProjectPath, project.ProjectName)}\".csproj")
                .InWorkingDirectory(context.Configuration.OutputPath)
                .Execute();

        //Add References
        foreach (var project in _projectTemplateService.GetProjects(model.Project))
        foreach (var reference in _projectTemplateService.GetReferences(project))
            ProcessCommand.Create("dotnet ", $"add \"{project.ProjectPath}\" reference \"{reference}\"")
                .InWorkingDirectory(context.Configuration.OutputPath)
                .Execute();
    }
}