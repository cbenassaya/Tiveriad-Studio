using Tiveriad.Commons.Extensions;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.Projects;
using Tiveriad.Studio.Generators.Services;
using Tiveriad.Studio.Generators.Sources;
using Tiveriad.TextTemplating;

namespace Tiveriad.Studio.Generators.Net.Middlewares;

public class ProjectFileMiddleware:IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>
{
    private readonly ITemplateRenderer _templateRenderer;
    private readonly IProjectTemplateService<InternalType, ProjectDefinition> _defaultProjectTemplateService;

    public ProjectFileMiddleware(ITemplateRenderer templateRenderer, IProjectTemplateService<InternalType, ProjectDefinition> defaultProjectTemplateService)
    {
        _templateRenderer = templateRenderer;
        _defaultProjectTemplateService = defaultProjectTemplateService;
    }

    public async void Run(PipelineContext context, PipelineModel model)
    {
        var sourcesItems = context.Properties.GetOrAdd("SourceItems", ()=> new List<SourceItem>()) as IList<SourceItem>;
        var internalTypes = context.Properties.GetOrAdd("InternalTypes", ()=> new List<InternalType>()) as IList<InternalType>;
        var projectDefinitions = _defaultProjectTemplateService.GetProjects(model.Project);
        
        foreach (var projectDefinition in projectDefinitions.Where(x=>x.Layer=="Api").ToArray())
        {
            
            var programSourceItemDependencies = new List<string>();
 
            programSourceItemDependencies.Add(projectDefinitions.FirstOrDefault(x=>x.Layer=="Infrastructure" && x.Module.Equals(projectDefinition.Module)).RootNamespace);
            programSourceItemDependencies.Add(projectDefinitions.FirstOrDefault(x=>x.Layer=="Application" && x.Module.Equals(projectDefinition.Module)).RootNamespace);
            programSourceItemDependencies.Add(projectDefinition.RootNamespace);
            
            var programSourceItem = SourceItem.Init()
                .WithDirectory(projectDefinition.ProjectPath)
                .WithName("Program.cs")
                .WithSource(await _templateRenderer.RenderAsync("Program.tpl", new {itemnamespace = projectDefinition.RootNamespace, dependencies = programSourceItemDependencies}));
            sourcesItems.Add(programSourceItem);
            
            
            var extensionsSourceItemDependencies = new List<string>();
 
            extensionsSourceItemDependencies.Add(projectDefinitions.FirstOrDefault(x=>x.Layer=="Persistence" && x.Module.Equals(projectDefinition.Module)).RootNamespace);
            var extensionsSourceItem = SourceItem.Init()
                .WithDirectory(projectDefinition.ProjectPath)
                .WithName("Extensions.cs")
                .WithSource(await _templateRenderer.RenderAsync("Extensions.tpl", new {itemnamespace = projectDefinition.RootNamespace, dependencies = extensionsSourceItemDependencies}));
            sourcesItems.Add(extensionsSourceItem);
            
            
            var transactionActionFilterSourceItem = SourceItem.Init()
                .WithDirectory(Path.Combine(projectDefinition.ProjectPath,"Filters"))
                .WithName("TransactionActionFilter.cs")
                .WithSource(await _templateRenderer.RenderAsync("TransactionActionFilter.tpl", new {itemnamespace = $"{projectDefinition.RootNamespace}.Filters" }));
            sourcesItems.Add(transactionActionFilterSourceItem);
        }

        foreach (var projectDefinition in projectDefinitions.Where(x => x.Layer == "Application").ToArray())
        {
            var extensionsSourceItemDependencies = new List<string>();
 
            extensionsSourceItemDependencies.Add(projectDefinitions.FirstOrDefault(x=>x.Layer=="Persistence" && x.Module.Equals(projectDefinition.Module)).RootNamespace);
            var extensionsSourceItem = SourceItem.Init()
                .WithDirectory(projectDefinition.ProjectPath)
                .WithName("Extensions.cs")
                .WithSource(await _templateRenderer.RenderAsync("ApplicationDependencyInjection.tpl", new {itemnamespace = projectDefinition.RootNamespace, dependencies = extensionsSourceItemDependencies}));
            sourcesItems.Add(extensionsSourceItem);
        }
        
        
        foreach (var projectDefinition in projectDefinitions.Where(x => x.Layer == "Infrastructure").ToArray())
        {
            var extensionsSourceItemDependencies = new List<string>();
            extensionsSourceItemDependencies.Add(projectDefinitions.FirstOrDefault(x=>x.Layer=="Persistence" && x.Module.Equals(projectDefinition.Module)).RootNamespace);
            var entity = model.Project.GetChildren<XEntity>().FirstOrDefault(x => x.GetModule().Equals(projectDefinition.Module));
            var internalType = internalTypes.FirstOrDefault(x => x.Reference!=null && x.Reference.Equals(entity));
            extensionsSourceItemDependencies.Add(internalType.Namespace);
            var extensionsSourceItem = SourceItem.Init()
                .WithDirectory(projectDefinition.ProjectPath)
                .WithName("Extensions.cs")
                .WithSource(await _templateRenderer.RenderAsync("InfrastructureDependencyInjection.tpl", 
                    new
                    {
                        itemnamespace = projectDefinition.RootNamespace,
                        firstentity=entity.Name,
                        dependencies = extensionsSourceItemDependencies
                    }));
            sourcesItems.Add(extensionsSourceItem);
        }
        
        foreach (var projectDefinition in projectDefinitions.Where(x => x.Layer == "Persistence").ToArray())
        {
            var extensionsSourceItem = SourceItem.Init()
                .WithDirectory(projectDefinition.ProjectPath)
                .WithName("DefaultContext.cs")
                .WithSource(await _templateRenderer.RenderAsync("DefaultContext.tpl", new {itemnamespace = projectDefinition.RootNamespace}));
            sourcesItems.Add(extensionsSourceItem);
        }
        
    }
}