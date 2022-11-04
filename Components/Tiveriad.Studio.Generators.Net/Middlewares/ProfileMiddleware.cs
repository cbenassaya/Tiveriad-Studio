using System.Security.Cryptography;
using Tiveriad.Commons.Extensions;
using Tiveriad.Pipelines;
using Tiveriad.Studio.Application.Pipelines;
using Tiveriad.Studio.Core.Entities;
using Tiveriad.Studio.Core.Extensions;
using Tiveriad.Studio.Core.Processors;
using Tiveriad.Studio.Generators.Models;
using Tiveriad.Studio.Generators.Net.Projects;
using Tiveriad.Studio.Generators.Services;
using Tiveriad.Studio.Generators.Sources;
using Tiveriad.TextTemplating;

namespace Tiveriad.Studio.Generators.Net.Middlewares;

public class ProfileMiddleware :  IMiddleware<PipelineModel, PipelineContext, PipelineConfiguration>, IProcessor
{
    
    private readonly ITemplateRenderer _templateRenderer;
    private readonly IProjectTemplateService<InternalType, ProjectDefinition> _defaultProjectTemplateService;

    public ProfileMiddleware(IProjectTemplateService<InternalType, ProjectDefinition> defaultProjectTemplateService, ITemplateRenderer templateRenderer)
    {
        _defaultProjectTemplateService = defaultProjectTemplateService;
        _templateRenderer = templateRenderer;
    }

    public  Task Run(PipelineContext context, PipelineModel model)
    {
        var sourcesItems = context.Properties.GetOrAdd("SourceItems", ()=> new List<SourceItem>()) as IList<SourceItem>;
        var internalTypes = context.Properties.GetOrAdd("InternalTypes", ()=> new List<InternalType>()) as IList<InternalType>;
        var projectDefinitions = _defaultProjectTemplateService.GetProjects(model.Project);
        var entities = model.Project.GetChildren<XEntity>();
        foreach (var entity in entities)
        {
            var mappings = model.Project.GetChildren<XEndPoint>().SelectMany(x => x.Mappings).Where(x=>x.To.Equals(entity) || x.From.Equals(entity)).Distinct().ToList();
            var dependencies = new List<string>();
            foreach (var mapping in mappings)
            {
                var internalType = internalTypes.FirstOrDefault(x => x.Reference!=null && x.Reference.Equals(mapping.From));
                if (!dependencies.Contains(internalType.Namespace))
                    dependencies.Add(internalType.Namespace);
            }
            foreach (var mapping in mappings)
            {
                var internalType = internalTypes.FirstOrDefault(x => x.Reference!=null && x.Reference.Equals(mapping.To));
                if (!dependencies.Contains(internalType.Namespace))
                    dependencies.Add(internalType.Namespace);
            }
            var projectDefinition =
                projectDefinitions.FirstOrDefault(x => x.Module.ReferenceId.Equals(entity.GetModule().ReferenceId));


            var source = _templateRenderer.RenderAsync("Profile.tpl",
                new
                {
                    itemnamespace = $"{projectDefinition.RootNamespace}.Mappings", entity = entity, mappings = mappings,
                    dependencies = dependencies
                });
            
            var transactionActionFilterSourceItem = SourceItem.Init()
                .WithDirectory(Path.Combine(projectDefinition.ProjectPath,"Mappings"))
                .WithName($"{entity.Name}Profile.cs")
                .WithSource( source.Result);
            sourcesItems.Add(transactionActionFilterSourceItem);
            
        }
        return Task.CompletedTask;
    }
}